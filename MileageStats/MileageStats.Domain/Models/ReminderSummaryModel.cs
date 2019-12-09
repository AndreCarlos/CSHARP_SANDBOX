//===================================================================================
// Microsoft patterns & practices
// Silk : Web Client Guidance
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
using System;
using MileageStats.Model;

namespace MileageStats.Domain.Models
{
    public class ReminderSummaryModel
    {
        private readonly Reminder _reminder;
        private readonly bool _isOvedue;

        public ReminderSummaryModel(Reminder reminder, bool isOvedue)
        {
            _reminder = reminder;
            _isOvedue = isOvedue;
        }

        public int ReminderId
        {
            get { return _reminder.ReminderId; }
        }

        public string Title
        {
            get { return _reminder.Title; }
        }

        public bool IsOverdue
        {
            get { return _isOvedue; }
        }

        public string DueOnFormatted
        {
            get
            {
                var msg = _reminder.DueDate == null
                                 ? string.Empty
                                 : String.Format("on {0:d MMM yyyy}", _reminder.DueDate);

                msg += _reminder.DueDate == null || _reminder.DueDistance == null
                           ? string.Empty
                           : " or ";

                msg += _reminder.DueDistance == null
                           ? string.Empty
                           : String.Format("at {0} miles", _reminder.DueDistance);

                return msg + ".";
            }
        }
    }
}