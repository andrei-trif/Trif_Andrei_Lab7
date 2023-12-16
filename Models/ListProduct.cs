using SQLite;
using SQLiteNetExtensions.Attributes;
using Trif_Andrei_Lab7.Data;

namespace Trif_Andrei_Lab7.Models
{
    public class ListProduct
    {
        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; }

        [ForeignKey(typeof(ShopList))] 
        public int ShopListID { get; set; }
        
        public int ProductID { get; set; }
    }
}
