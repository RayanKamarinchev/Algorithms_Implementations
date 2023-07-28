#include <iostream>
#include <vector>
using namespace std;

int maxArea(vector<int>& height) {
	int j = height.size() - 1;
	int water = 0;
	for (int i = 0; i <= j; i++)
	{
		water = max(water, (j - i) * min(height[i], height[j]));
		if (height[i] > height[j])
			j++;
		else
			i++;
	}
	return water;
}

int main()
{
    vector<int> arr {1, 8, 6, 2, 5, 4, 8, 3, 7};
    cout << maxArea(arr);
}