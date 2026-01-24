/// <summary>
/// Defines a maze using a dictionary. The dictionary is provided by the
/// user when the Maze object is created. The dictionary will contain the
/// following mapping:
///
/// (x,y) : [left, right, up, down]
///
/// If a direction is false, then we can assume there is a wall in that direction.
/// If a direction is true, then we can proceed.
///
/// If there is a wall, then throw an InvalidOperationException with the message
/// "Can't go that way!".  If there is no wall, then the 'currX' and 'currY'
/// values should be changed.
/// </summary>
public class Maze
{
    private readonly Dictionary<ValueTuple<int, int>, bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<ValueTuple<int, int>, bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    private bool[] CurrentCellMoves()
    {
        if (!_mazeMap.TryGetValue((_currX, _currY), out var moves) || moves == null || moves.Length != 4)
            throw new InvalidOperationException("Can't go that way!");
        return moves;
    }

    /// <summary>
    /// Check to see if you can move left. If you can, then move.
    /// If you can't move, throw an InvalidOperationException with "Can't go that way!".
    /// </summary>
    public void MoveLeft()
    {
        var moves = CurrentCellMoves();
        if (!moves[0]) throw new InvalidOperationException("Can't go that way!");
        _currX -= 1;
    }

    /// <summary>
    /// Check to see if you can move right. If you can, then move.
    /// If you can't move, throw an InvalidOperationException with "Can't go that way!".
    /// </summary>
    public void MoveRight()
    {
        var moves = CurrentCellMoves();
        if (!moves[1]) throw new InvalidOperationException("Can't go that way!");
        _currX += 1;
    }

    /// <summary>
    /// Check to see if you can move up. If you can, then move.
    /// If you can't move, throw an InvalidOperationException with "Can't go that way!".
    /// </summary>
    public void MoveUp()
    {
        var moves = CurrentCellMoves();
        if (!moves[2]) throw new InvalidOperationException("Can't go that way!");
        _currY -= 1; // Up is y-1 in the provided tests
    }

    /// <summary>
    /// Check to see if you can move down. If you can, then move.
    /// If you can't move, throw an InvalidOperationException with "Can't go that way!".
    /// </summary>
    public void MoveDown()
    {
        var moves = CurrentCellMoves();
        if (!moves[3]) throw new InvalidOperationException("Can't go that way!");
        _currY += 1; // Down is y+1 in the provided tests
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}
