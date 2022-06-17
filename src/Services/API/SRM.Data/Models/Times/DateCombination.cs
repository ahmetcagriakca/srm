using Fix.Data;

namespace SRM.Data.Models.Times
{
    //Günlerin ilişki matris tablosu
    public class DateCombination : FixEntity<int>
    {
        //Pazartesi

        public bool Monday { get; set; }

        //Salı
        public bool Tuesday { get; set; }
        //Çarşamba
        public bool Wednesday { get; set; }

        //Perşembe
        public bool Thursday { get; set; }
        //Cuma
        public bool Friday { get; set; }

        //Cumartesi
        public bool Saturday { get; set; }

        //Pazar
        public bool Sunday { get; set; }
    }

}