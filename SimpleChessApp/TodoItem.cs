namespace SimpleChessApp
{
    class TodoItem
    {
        public bool Done { get; set; }
        public string Name { get; set; }
        public Difficulty? Effort { get; set; }

        public TodoItem(bool c, Difficulty? e, string n)
        {
            Done = c;
            Name = n;
            Effort = e;
        }
    }

    enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

}

