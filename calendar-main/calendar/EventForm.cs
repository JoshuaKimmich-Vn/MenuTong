using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using carlender.Models;

namespace carlender
{
    public partial class EventForm : Form
    {
        private DateTime selectedDate;

        private ScheduleEvent? editingEvent;

        public ScheduleEvent? ScheduleEvent { get; private set; }

        public EventForm(DateTime selectedDate)
        {
            InitializeComponent();

            this.selectedDate = selectedDate;

            cboCategory.Items.Add("Học tập");
            cboCategory.Items.Add("Công việc");
            cboCategory.Items.Add("Cá nhân");
            cboCategory.Items.Add("Khác");

            cboCategory.SelectedIndex = 0;

            dtpStartTime.Value = selectedDate.Date.AddHours(7);
            dtpEndTime.Value = selectedDate.Date.AddHours(8);
        }
        public EventForm(ScheduleEvent scheduleEvent)
        {
            InitializeComponent();

            editingEvent = scheduleEvent;

            cboCategory.Items.Add("Học tập");
            cboCategory.Items.Add("Công việc");
            cboCategory.Items.Add("Cá nhân");
            cboCategory.Items.Add("Khác");

            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.CustomFormat = "dd/MM/yyyy HH:mm";

            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.CustomFormat = "dd/MM/yyyy HH:mm";

            txtTitle.Text = scheduleEvent.Title;
            dtpStartTime.Value = scheduleEvent.StartTime;
            dtpEndTime.Value = scheduleEvent.EndTime;
            txtDescription.Text = scheduleEvent.Description;
            cboCategory.Text = scheduleEvent.Category;

            Text = "Sửa sự kiện";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Vui lòng nhập tiêu đề sự kiện.");
                return;
            }

            if (dtpStartTime.Value >= dtpEndTime.Value)
            {
                MessageBox.Show("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.");
                return;
            }

            ScheduleEvent = new ScheduleEvent(
                txtTitle.Text,
                dtpStartTime.Value,
                dtpEndTime.Value,
                txtDescription.Text,
                cboCategory.Text
            );

            if (editingEvent != null)
            {
                ScheduleEvent.Id = editingEvent.Id;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}