#include <iostream>
#include <vector>
#include <numeric>
#include <sstream>
using namespace std;

bool canPartition(vector<int>& nums, vector<int>& subsetSum, int targetSum, int currentIndex) {
    if (currentIndex < 0) {
        for (int i = 0; i < 3; ++i) {
            if (subsetSum[i] != targetSum)
                return false;
        }
        return true;
    }
    
    for (int i = 0; i < 3; ++i) {
        if (subsetSum[i] + nums[currentIndex] <= targetSum) {
            subsetSum[i] += nums[currentIndex];
            if (canPartition(nums, subsetSum, targetSum, currentIndex - 1))
                return true;
            subsetSum[i] -= nums[currentIndex];
        }
    }

    return false;
}

bool canPartitionIntoThreeEqualParts(vector<int>& nums) {
    int sum = 0;
    for (int num : nums) {
        sum += num;
    }

    if (sum % 3 != 0)
        return false;

    int targetSum = sum / 3;
    vector<int> subsetSum(3, 0);
    return canPartition(nums, subsetSum, targetSum, nums.size() - 1);
}
int main()
{
    string myString;
    cin >> myString;
    stringstream iss(myString);

    int number;
    std::vector<int> myNumbers;
    while (iss >> number)
        myNumbers.push_back(number);
    cout << canPartitionIntoThreeEqualParts(myNumbers);
}