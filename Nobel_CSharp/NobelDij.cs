
namespace Nobel_CSharp
{
    internal class NobelDij
    {
        public int Year { get; set; }
        public string Tipus { get; set; }
        public string Keresztnev { get; set; }  
        public string Vezeteknev { get; set; }

        public override string ToString()
        {
            return $"Év: {Year}, Típus: {Tipus}, Név: {Keresztnev} {Vezeteknev}";
        }
    }
}
