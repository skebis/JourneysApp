namespace solita_assignment.Classes
{
    /// <summary>
    /// Pagination class that provides information about which page is being observed and how many
    /// entities are on a single page.
    /// </summary>
    public class Pagination
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
