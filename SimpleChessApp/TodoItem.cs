using System.Collections.Generic;

namespace SimpleChessApp
{
    class TodoItem
    {
        public bool Done { get; set; }
        public Difficulty? Effort { get; set; }
        public string Name { get; set; }

        public TodoItem(bool c, Difficulty? e, string n)
        {
            Done = c;
            Name = n;
            Effort = e;
        }

        public static List<TodoItem> Items
        {
            get
            {
                var x = new List<TodoItem>();
                // Implemented
                x.Add(new TodoItem(false, null, "--------- DONE ---------"));
                x.Add(new TodoItem(true, Difficulty.Easy, "1 Selection"));
                x.Add(new TodoItem(true, Difficulty.Easy, "2 General Move with Click & Click"));
                x.Add(new TodoItem(true, Difficulty.Easy, "2.1 Pawn movement"));
                x.Add(new TodoItem(true, Difficulty.Easy, "3 General Capture"));
                x.Add(new TodoItem(true, Difficulty.Easy, "3.1 Avoid capturing of same color"));
                x.Add(new TodoItem(true, Difficulty.Medium, "4 Pawn promotion"));
                // Not implemented
                x.Add(new TodoItem(false, null, ""));
                x.Add(new TodoItem(false, null, "--------- TO DO ---------"));
                x.Add(new TodoItem(false, null, "1 Game Flow"));
                x.Add(new TodoItem(false, Difficulty.Easy, "1.1 Turns"));
                x.Add(new TodoItem(false, Difficulty.Easy, "1.2 Reset board"));
                x.Add(new TodoItem(false, null, "2 Movement"));
                x.Add(new TodoItem(false, Difficulty.Medium, "2.1 Movement rules for each piece"));
                x.Add(new TodoItem(false, Difficulty.Hard, "2.2 Handle move interception"));
                x.Add(new TodoItem(false, null, "2.3 Special moves"));
                x.Add(new TodoItem(false, Difficulty.Easy, "2.3.1 King castle"));
                x.Add(new TodoItem(false, Difficulty.Medium, "2.3.2 Check"));
                x.Add(new TodoItem(false, null, "3. End Game"));
                x.Add(new TodoItem(false, Difficulty.Hard, "3.1 Stale Mate"));
                x.Add(new TodoItem(false, Difficulty.Hard, "3.2 Check Mate"));
                x.Add(new TodoItem(false, Difficulty.Easy, "3.3 Draw by repetition"));
                x.Add(new TodoItem(false, Difficulty.Medium, "4 En Passant"));
                x.Add(new TodoItem(false, Difficulty.Easy, "5 Chess annotation"));

                return x;
            }
        }
    }

    enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
}

