namespace solita_assignment.Classes
{
    // Represents the http get reponse structure.
    public class PagedResponse<T> where T : class
    {
        public int DataCount { get; set; }

        public List<T> Data { get; set; } = new List<T>();
    }
}
