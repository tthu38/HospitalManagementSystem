using System;
using System.Collections.Generic;
using System.Linq;
using Business;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AdmissionDAO
    {
        private static AdmissionDAO? instance;
        private static readonly object lockObj = new();

        private AdmissionDAO() { }

        public static AdmissionDAO Instance
        {
            get
            {
                lock (lockObj)
                {
                    return instance ??= new AdmissionDAO();
                }
            }
        }

        public List<Admission> GetAll()
        {
            using var context = new HospitalManagementContext();
            return context.Admissions
                .Include(a => a.Patient)
                .Include(a => a.Room)
                .OrderByDescending(a => a.AdmissionDate)
                .ToList();
        }

        public Admission? GetById(int id)
        {
            using var context = new HospitalManagementContext();
            return context.Admissions
                .Include(a => a.Patient)
                .Include(a => a.Room)
                .FirstOrDefault(a => a.AdmissionId == id);
        }

        public void Add(Admission admission)
        {
            using var context = new HospitalManagementContext();
            admission.Status ??= "Active";
            if (admission.AdmissionDate == default)
                admission.AdmissionDate = DateTime.Now;

            context.Admissions.Add(admission);

            var room = context.Rooms.FirstOrDefault(r => r.RoomId == admission.RoomId);
            if (room != null)
                room.IsAvailable = false;

            context.SaveChanges();
        }

        public void Discharge(int admissionId)
        {
            using var context = new HospitalManagementContext();
            var adm = context.Admissions.Include(a => a.Room).FirstOrDefault(a => a.AdmissionId == admissionId);
            if (adm == null) return;

            adm.DischargeDate = DateTime.Now;
            adm.Status = "Discharged";
            if (adm.Room != null)
                adm.Room.IsAvailable = true;

            context.SaveChanges();

            // Tự động tạo bill khi xuất viện
            var billDAO = new BillingDAO();
            billDAO.GenerateForAdmission(admissionId);
        }
    }
}
