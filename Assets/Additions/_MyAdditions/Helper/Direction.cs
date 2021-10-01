namespace KAP.Helper
{
    public static class Direction
    {
        public enum Directions { Up, Down, Left, Right, Error };

        #region Properties
        public static Directions Up => Directions.Up;
        public static Directions Down => Directions.Down;
        public static Directions Left => Directions.Left;
        public static Directions Right => Directions.Right;
        #endregion

        public static Directions GiveRightDirection(Directions forwardDirection)
        {
            switch (forwardDirection)
            {
                case Directions.Up: return Right;
                case Directions.Down: return Left;
                case Directions.Left: return Up;
                case Directions.Right: return Down;
            }

            return Directions.Error;
        }

        public static Directions GiveLeftDirection(Directions forwardDirection)
        {
            switch (forwardDirection)
            {
                case Directions.Up: return Left;
                case Directions.Down: return Right;
                case Directions.Left: return Down;
                case Directions.Right: return Up;
            }

            return Directions.Error;
        }

        public static Directions GiveBackDirection(Directions forwardDirection)
        {
            switch (forwardDirection)
            {
                case Directions.Up: return Down;
                case Directions.Down: return Up;
                case Directions.Left: return Right;
                case Directions.Right: return Left;
            }

            return Directions.Error;
        }

        public static Directions Random()
        {
            System.Array values = System.Enum.GetValues(typeof(Directions));
            System.Random random = new System.Random();
            Directions temp =  (Directions)values.GetValue(random.Next(values.Length));
            if (temp == Directions.Error)
                temp = Random();
            return temp;
        }
    }
}
