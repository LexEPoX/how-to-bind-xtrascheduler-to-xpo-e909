﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.Xpo;

namespace XtraScheduler_XPO {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            schedulerStorage1.Appointments.DataSource = this.xpCollection1;

            schedulerStorage1.Appointments.Mappings.AllDay = "AllDay";
            schedulerStorage1.Appointments.Mappings.Description = "Description";
            schedulerStorage1.Appointments.Mappings.End = "Finish";
            schedulerStorage1.Appointments.Mappings.Label = "Label";
            schedulerStorage1.Appointments.Mappings.Location = "Location";
            schedulerStorage1.Appointments.Mappings.RecurrenceInfo = "Recurrence";
            schedulerStorage1.Appointments.Mappings.ReminderInfo = "Reminder";
            schedulerStorage1.Appointments.Mappings.Start = "Start";
            schedulerStorage1.Appointments.Mappings.Status = "Status";
            schedulerStorage1.Appointments.Mappings.Subject = "Subject";
            schedulerStorage1.Appointments.Mappings.Type = "AppointmentType";
            schedulerStorage1.Appointments.Mappings.ResourceId = "ResourceId";

            schedulerStorage1.Resources.Mappings.Id = "ResourceId";
            schedulerStorage1.Resources.Mappings.Caption = "Name";
            schedulerStorage1.Resources.Mappings.Color = "Color";


            schedulerControl1.Start = DateTime.Today;
            schedulerControl1.GroupType = SchedulerGroupType.Resource;
            FillData();
        }


        private void OnAppointmentsChanged(object sender, PersistentObjectsEventArgs e) {
            foreach(Appointment apt in e.Objects) {
                XPBaseObject o = apt.GetSourceObject((SchedulerStorage)sender) as XPBaseObject;
                if(o != null)
                    o.Save();
            }
        }

        void FillData()
        {
            if (this.schedulerStorage1.Resources.Count == 0){
                Resource res1 = this.schedulerStorage1.CreateResource(1);
                res1.Caption = "First Resource";
                res1.Color = Color.LightSkyBlue;
                this.schedulerStorage1.Resources.Add(res1);
                Resource res2 = this.schedulerStorage1.CreateResource(2);
                res2.Caption = "Next Resource";
                res2.Color = Color.LightYellow;
                this.schedulerStorage1.Resources.Add(res2);
            }

            if (this.schedulerStorage1.Appointments.Count == 0)
            {
                Appointment apt1 = this.schedulerStorage1.CreateAppointment(AppointmentType.Normal);
                apt1.Start = DateTime.Now;
                apt1.End = apt1.Start.AddHours(2.5f);
                apt1.Subject = "First Appointment";
                apt1.LabelId = 1;
                apt1.ResourceId = this.schedulerStorage1.Resources[0].Id;
                this.schedulerStorage1.Appointments.Add(apt1);
            }
            
            }

        private void OnResourcesChanged(object sender, PersistentObjectsEventArgs e)
        {
            foreach (Resource res in e.Objects)
            {
                XPBaseObject o = res.GetSourceObject((SchedulerStorage)sender) as XPBaseObject;
                if (o != null)
                    o.Save();
            }
        }
        
        }

    }