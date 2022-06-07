using VisualLogic.Elements;

SortLogicApp.Run(25, 20);

void solution(VisualArray array)
{
    bool ordenado = false;
    while (!ordenado)
    {
        ordenado = true;
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i] < array[i + 1])
            {
                var temp = array[i];
                array[i] = array[i + 1];
                array[i + 1] = temp;
                ordenado = false;
            }
        }
    }
}