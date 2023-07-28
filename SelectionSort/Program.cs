int[] arr = new int[] {64, 25, 12, 22, 11};

void SelectionSort()
{
    for (int i = 0; i < arr.Length; i++)
    {
        int currElement = arr[i];
        int min = currElement;
        int minIndex = i;
        for (int j = i + 1; j < arr.Length; j++)
        {
            if (arr[j] < min)
            {
                min = arr[j];
                minIndex = j;
            }
        }

        arr[minIndex] = currElement;
        arr[i] = min;
    }
}

void printArray(int[] arr)
{
    int n = arr.Length;
    for (int i = 0; i < n; ++i)
        Console.Write(arr[i] + " ");
    Console.WriteLine();
}

SelectionSort();
printArray(arr);