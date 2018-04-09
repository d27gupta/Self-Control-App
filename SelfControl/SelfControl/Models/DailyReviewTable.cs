﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfControl.Models
{
    [Table("DailyReview")]
    public class DailyReviewTable
    {
        static string DATETIME_COL = "_date";
        static string ID_COL = "_id";
        static string RESPONSE_COL = "_response";

        public static string dateTimeCol { get => DATETIME_COL; }
        public static string idCol { get => ID_COL; }
        public static string responseCol { get => RESPONSE_COL; }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        [Column("_date")]
        public DateTime DATE { get; set; }
        [Column("_response")]
        public string RESPONSE { get; set; }
    }
}
