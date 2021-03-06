﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfControl.Models
{
    [Table("WeeklyReview")]
    public class WeeklyReviewTable
    {
        static string DATETIME_COL = "_date";
        static string ID_COL = "_id";
        static string RESPONSE_COL = "_response";
        static string WEEK_COL = "_week";
        static string ISCOMPLETED_COL = "_isCompleted";

        public static string dateTimeCol { get => DATETIME_COL; }
        public static string idCol { get => ID_COL; }
        public static string responseCol { get => RESPONSE_COL; }
        public static string weekCol { get => WEEK_COL; }
        public static string isCompletedCol { get => ISCOMPLETED_COL; }

        [Column("_id")]
        public int ID { get; set; }
        [Column("_date")]
        public DateTime DATE { get; set; }
        [Column("_response")]
        public string RESPONSE { get; set; }
        [Column("_week")]
        public int WEEK { get; set; }
        [Column("_isCompleted")]
        public bool ISCOMPLETED { get; set; }
    }
}
