
namespace Nobel_CSharp
{
    internal class NobelDij
    {
        public int Ev { get; set; }
        public string Tipus { get; set; }
        public string Keresztnev { get; set; }  
        public string Vezeteknev { get; set; }

        public override string ToString()
        {
            return $"Év: {Ev}, Típus: {Tipus}, Név: {Keresztnev} {Vezeteknev}";
        }
    }
}
