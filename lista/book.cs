namespace lista
{
    [Serializable]
    internal class Book
    {
        public Book(){}
        public int Id { get; set; }
        public string Title { get; set; }    
        public string Autor { get; set; }
        public int RealeaseYear { get; set; }
    }
}
