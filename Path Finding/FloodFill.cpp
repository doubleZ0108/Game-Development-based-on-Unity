class Grid{
public:
    Grid(int initValue);
    void reset(int value);
    inline int getValue(int x, int y) const {return mValue[x][y]; }
    inline void setValue(int x, int y, int value) { mValue[x][y] = value; }
    void computeFloodFill(const Grid & collision, int startX, int startY);
private:
    void _compiteFloodFill(const Grid& collision, int x, inty, int value);
    
    static const int WIDTH = 15;
    static const int HEIGHT = 15;
    int mValue[WIDTH][HEIGHT];
};

Grid::Grid(int initValue)
{
    reset(initValue);
}

void Grid::reset(int value)
{
    memset(mValue, value, sizeof(mValue));
}

void Grid::computeFloodFill(const Grid& collision, int startX, int startY)
{
    reset(-1);
    setValue(start, startY, 0);
    _compiteFloodFill(collision, startX, startY, 0);
}


void Grid::_compiteFloodFill(const Grid& collision, int x, int y, int value)
{
    static const int DirX[] = {-1, 0, 1, 0};
    static const int DirY[] = {0, -1, 0, -1};
    
    int nextValue = value + 1;
    // Flood
    for(int i=0;i<4;i++)
    {
        int nextX = x + DirX[i];
        int nextY = y + DirY[i];
        
        if(nextX >=0 && nextX < WIDTH &&
           nextY >=0 && nextY < HEIGHT &&
           collision.getValue(nextX, nextY) != 1 &&  //墙
           (getValue(nextX, nextY) == -1 || getValue(nextX, nextY > nextValue)))
        {
            setValue(nextX, nextY, nextValue);
        }
    }
    
    for(int i=0;i<4;i++)
    {
        int nextX = x + DirX[i];
        int nextY = y + DirY[i];
        if(getValue(nextX, nextY == nextValue))
        {
            _compiteFloodFill(collision, nextX, nextY, nextValue);
        }
    }
}
