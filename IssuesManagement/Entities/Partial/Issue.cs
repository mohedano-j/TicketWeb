namespace IssuesManagement
{
    public partial class Issue
    {
        public string Status => StatusOpened ? "Opened" : "Closed";
    }
}