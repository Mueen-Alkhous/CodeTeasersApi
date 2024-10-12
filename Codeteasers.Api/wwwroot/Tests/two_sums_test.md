import unittest

from two_sums_template import two_sum

class TestTwoSum(unittest.TestCase):
    def test_1(self):
        'Case 1'
        self.assertEqual(sorted(two_sum([2,7,11,15], 9)), [1, 1])
    def test_2(self):
        'Case 2'
        self.assertEqual(sorted(two_sum([2,3,11,15], 5)), [0, 1])

if __name__ == "__main__":
    unittest.main()