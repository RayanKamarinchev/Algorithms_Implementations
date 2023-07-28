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

void Merge(int[] arr, int left, int middle, int right)
{
    int size1 = middle - left + 1;
    int size2 = right - middle;

    int[] leftArr = new int[size1];
    int[] rightArr = new int[size2];

    for (int l = 0; l < size1; l++)
    {
        leftArr[l] = arr[left + l];
    }
    for (int l = 0; l < size2; l++)
    {
        rightArr[l] = arr[middle + 1 + l];
    }

    int i = 0, j = 0;

    int k = left;
    while (i < size1 && j < size2)
    {
        if (leftArr[i] <= rightArr[j])
        {
            arr[k] = leftArr[i];
            i++;
        }
        else
        {
            arr[k] = rightArr[j];
            j++;
        }
        k++;
    }

    while (i < size1)
    {
        arr[k] =leftArr[i];
        i++;
        k++;
    }
    
    while (j < size2)
    {
        arr[k] = rightArr[j];
        j++;
        k++;
    }
}

void MergeSort(int[] arr, int left, int right)
{
    if (left < right)
    {
        int middle = left + (right - left)/2;

        MergeSort(arr, left, middle);
        MergeSort(arr, middle+1, right);

        Merge(arr, left, middle, right);
    }
}

int Partition(int[] arr, int low, int high)
{
    int pivot = arr[high];

    int i = low - 1;
    for (int j = low; j < high - 1; j++)
    {
        if (arr[j] < pivot)
        {
            i++;
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }
    }

    i++;
    (arr[high], arr[i]) = (arr[i], arr[high]);
    return i;
}

void QuickSort(int[] arr, int low, int high)
{
    if (low < high)
    {
        int partitionIndex = Partition(arr, low, high);

        QuickSort(arr, low, partitionIndex-1);
        QuickSort(arr, partitionIndex+1, high);
    }
}

void printArray(int[] arr)
{
    int n = arr.Length;
    for (int i = 0; i < n; ++i)
        Console.Write(arr[i] + " ");
    Console.WriteLine();
}

QuickSort(arr, 0, arr.Length-1);
printArray(arr);