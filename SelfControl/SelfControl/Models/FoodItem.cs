﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfControl.Models
{
    [Table("FoodItems")]
    public class FoodItem
    {
        static string DATETIME_COL = "_date";
        static string ID_COL = "_id";
        static string name_col = "_name";
        static string PATH_COL = "_path";
        static string COOL_COL = "_coolEffect";
        static string HOT_COL = "_hotEffect";

        public static string dateTimeCol { get => DATETIME_COL; }
        public static string idCol { get => ID_COL; }
        public static string nameCol { get => name_col; }
        public static string pathCol { get => PATH_COL; }
        public static string coolCol { get => COOL_COL; }
        public static string hotCol { get => HOT_COL; }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        [Column("_date")]
        public DateTime DATE { get; set; }
        [Column("_name")]
        public string NAME { get; set; }
        [Column("_path")]
        public string PATH { get; set; }
        [Column("_coolEffect")]
        public bool COOLEFFECT { get; set; }
        [Column("_hotEffect")]
        public bool HOTEFFECT { get; set; }
    }
}
