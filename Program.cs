using System;
using System.Reflection;

namespace Week1
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }

        public static ListNode ArrayToListNode(int[] array)
        {
            if (array == null || array.Length == 0)
                return null;

            // Initialize the head node
            ListNode head = new ListNode(array[0]);
            ListNode current = head;

            // Loop through the array starting from the second element
            for (int i = 1; i < array.Length; i++)
            {
                current.next = new ListNode(array[i]);
                current = current.next;
            }

            return head;
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }

        public static TreeNode ArrayToTreeNode(object[] array)
        {
            if (array is null || array.Length == 0)
                return null;
            TreeNode head = new((int)array[0]);
            return CreateTreeNode(array);
        }
        private static TreeNode CreateTreeNode(object[] array, int index = 0)
        {

            if (index >= array.Length)
            {
                return null;
            }
            if (array[index] is not null)
            {
                TreeNode node = new TreeNode((int)array[index]);

                node.left = CreateTreeNode(array, 2 * index + 1);
                node.right = CreateTreeNode(array, 2 * index + 2);
                return node;
            }
            return null;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            //two sum =============================================
            //ExecuteTwoSum(new int[] { 2, 7, 11, 15 }, 9);
            //valid  parenthesis =============================================
            //IsValidParenthesis("{}[]");
            //merging two sorted link nodes =============================================
            //var arr1 = ListNode.ArrayToListNode(new int[] { });
            //var arr2 = ListNode.ArrayToListNode(new int[] { 0 });
            //var result = new ListNode();
            //MergeTwoSortedListNode(arr1, arr2, ref result);
            //max profit  =============================================
            //var array = new int[] { 2,4,1};
            //var result = MaxProfit(array);
            //is valid palindrom  =============================================
            //var s = "0P";
            //var result = IsPalindrome(s);
            //inverted tree node  =============================================
            //var arr = new int[] { 4, 2, 7, 1, 3, 6, 9 };
            //var node = TreeNode.ArrayToTreeNode(arr);
            //InvertTree(node); 
            //IsAnagram  =============================================
            //var s = "anagram";
            //var t = "nagaraa";
            //var result = IsAnagram(s, t);
            // binary search  =============================================
            //var arr = new int[] { -1, 0, 3, 5, 9, 12 };
            //var result = Search(arr, 9);
            //flood fill =============================================
            //int[][] sparseArray = new int[][]
            //{
            //new int[] { 0,0,0},
            //new int[] { 0,0,0 },
            //};

            //int sc = 0;
            //int sr = 1;
            //int c = 2;

            //var result = FloodFill(sparseArray, sr, sc, c);
            //Lowest Common Ancestor of a Binary Search Tree =============================================
            //var array = TreeNode.ArrayToTreeNode(new object[] { 2,1 });
            //var p = TreeNode.ArrayToTreeNode(new object[] {2});
            //var q = TreeNode.ArrayToTreeNode(new object[] { 1 });
            //var result = LowestCommonAncestor(array, p, q);

            //maximum sub array sum=============================================
            var array = new int[] { 5, 4, -1, 7, 8 };
            var result = MaxSubArray(array);
        }
        public static int[] ExecuteTwoSum(int[] nums, int target)
        {
            //without sorting we can find solution with differences
            var hash = new Dictionary<int, int>();
            var result = new int[2];
            for (int i = 0; i < nums.Length; i++)
            {
                var diff = target - nums[i];
                if (hash.ContainsKey(diff))
                {
                    result[0] = i;
                    result[1] = hash[diff];
                    break;
                }
                hash[nums[i]] = i;
            }
            return result;
        }
        public static bool IsValidParenthesis(string s)
        {
            if (s.Length % 2 != 0) return false;
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                    stack.Push('(');
                else if (s[i] == '{')
                    stack.Push('{');
                else if (s[i] == '[')
                    stack.Push('[');
                else
                {
                    if (stack.Count == 0)
                        return false;
                    var l = stack.Peek();
                    if ((s[i] == ')' && l == '(') || (s[i] == '}' && l == '{') || (s[i] == ']' && l == '['))
                        stack.Pop();
                    else
                        return false;
                }
            }
            return stack.Count == 0;
        }

        public static void MergeTwoSortedListNode(ListNode nodeA, ListNode nodeB, ref ListNode merged)
        {
            //result case
            if (nodeA is null && nodeB is null)
                return;
            //base case
            if (nodeA is null)
            {
                merged = nodeB;
                MergeTwoSortedListNode(nodeA, nodeB.next, ref merged.next);
            }
            else if (nodeB is null)
            {
                merged = nodeA;
                MergeTwoSortedListNode(nodeA.next, nodeB, ref merged.next);
            }
            else
            {
                if (nodeA.val < nodeB.val)
                {
                    merged = nodeA;
                    MergeTwoSortedListNode(nodeA.next, nodeB, ref merged.next);
                }
                else
                {
                    merged = nodeB;
                    MergeTwoSortedListNode(nodeA, nodeB.next, ref merged.next);
                }
            }
        }

        public static int MaxProfit(int[] prices)
        {
            int buyPrice = prices[0];
            int profit = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] < buyPrice)
                {
                    buyPrice = prices[i];
                }
                else
                {
                    var currentProfit = prices[i] - buyPrice;
                    profit = currentProfit > profit ? currentProfit : profit;
                }
            }
            return profit;
        }

        public static bool IsPalindrome(string s)
        {
            int asciiValueStartForLowerCase = 97;
            int asciiValueEndForLowerCase = 122;
            int rangeForUpperToLowerCase = 32;

            int asciiValueStartForNumber = 48 + rangeForUpperToLowerCase;
            int asciiValueEndForNumber = 57 + rangeForUpperToLowerCase;

            int left = 0;
            int right = s.Length - 1;

            while (left < right)
            {
                var chLeft = (int)s[left];
                chLeft = chLeft < asciiValueStartForLowerCase ? chLeft + rangeForUpperToLowerCase : chLeft;
                if (((chLeft < asciiValueStartForLowerCase) || chLeft > asciiValueEndForLowerCase) && (!(chLeft >= asciiValueStartForNumber && chLeft <= asciiValueEndForNumber)))
                {
                    left++;
                    continue;
                }
                var chRight = (int)s[right];
                chRight = chRight < asciiValueStartForLowerCase ? chRight + rangeForUpperToLowerCase : chRight;
                if (((chRight < asciiValueStartForLowerCase) || chRight > asciiValueEndForLowerCase) && (!(chRight >= asciiValueStartForNumber && chRight <= asciiValueEndForNumber)))
                {
                    right--;
                    continue;
                }
                if (chRight != chLeft)
                    return false;
                left++;
                right--;
            }
            return true;
        }
        public static TreeNode InvertTree(TreeNode root)
        {
            if (root is null)
                return null;

            (root.right, root.left) = (root.left, root.right);
            InvertTree(root.left);
            InvertTree(root.right);
            return root;
        }
        public static bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length) return false;
            Dictionary<int, int> hash = new Dictionary<int, int>();
            for (int i = 0; i < s.Length; i++)
                hash[s[i]] = hash.GetValueOrDefault(s[i]) + 1;
            for (int i = 0; i < t.Length; i++)
            {
                hash[t[i]] = hash.GetValueOrDefault(t[i]) - 1;
                if (hash[t[i]] == -1)
                    return false;
            }
            return true;
        }

        public static int Search(int[] nums, int target)
        {
            int l = 0;
            int r = nums.Length - 1;

            while (l <= r)
            {
                var m = l + (r - l) / 2;
                if (nums[m] == target) return m;
                if (nums[m] < target)
                    l = m + 1;
                else
                    r = m - 1;
            }

            return -1;
        }

        public static void FloodFillByDFS(int[][] image, int x, int y, int old, int color, int m, int n)
        {
            if (x >= m || y >= n) return;
            if (image[x][y] != old) return;

            image[x][y] = color; //ladnfilled by color

            if (x > 0) //upwards
                FloodFillByDFS(image, x - 1, y, old, color, m, n);
            if (y < n)
                FloodFillByDFS(image, x, y + 1, old, color, m, n);
            if (x < m)
                FloodFillByDFS(image, x + 1, y, old, color, m, n);
            if (y > 0) FloodFillByDFS(image, x, y - 1, old, color, m, n);
        }

        public static int[][] FloodFillByBFS(int[][] image, int x, int y, int old, int color, int m, int n)
        {
            Queue<KeyValuePair<int, int>> pos = new();
            pos.Enqueue(new KeyValuePair<int, int>(x, y));

            while (pos.Count > 0)
            {
                var pt = pos.Dequeue();
                if (pt.Key > 0 && image[pt.Key - 1][pt.Value] == old)
                {
                    image[pt.Key - 1][pt.Value] = color;
                    pos.Enqueue(new KeyValuePair<int, int>(pt.Key - 1, pt.Value));
                }
                if (pt.Value < n - 1 && image[pt.Key][pt.Value + 1] == old)
                {
                    image[pt.Key][pt.Value + 1] = color;
                    pos.Enqueue(new KeyValuePair<int, int>(pt.Key, pt.Value + 1));
                }
                if (pt.Key < m - 1 && image[pt.Key + 1][pt.Value] == old)
                {
                    image[pt.Key + 1][pt.Value] = color;
                    pos.Enqueue(new KeyValuePair<int, int>(pt.Key + 1, pt.Value));
                }
                if (pt.Value > 0 && image[pt.Key][pt.Value - 1] == old)
                {
                    image[pt.Key][pt.Value - 1] = color;
                    pos.Enqueue(new KeyValuePair<int, int>(pt.Key, pt.Value - 1));
                }
            }
            return image;
        }
        public static int[][] FloodFill(int[][] image, int sr, int sc, int color)
        {
            int prev = image[sr][sc];
            if (prev == color) return image;
            //FloodFillByDFS(image, sr, sc, prev, color, image.Length, image[0].Length);
            FloodFillByBFS(image, sr, sc, prev, color, image.Length, image[0].Length);
            return image;
        }
        //is greater than left node and smaller than right node
        public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (p.val > root.val && q.val > root.val)
                return LowestCommonAncestor(root.right, p, q);
            else if (p.val < root.val && q.val < root.val)
                return LowestCommonAncestor(root.left, p, q);
            return root;
        }
        public static int MaxSubArray(int[] nums)
        {
            int cSum = nums[0];
            int MSum = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                cSum += nums[i];
                int temp = Math.Max(cSum, nums[i]);
                if (temp > MSum)
                {
                    MSum = temp;
                    cSum = MSum;
                }
                else
                    cSum = temp;
            }
            return MSum ;
        }
    }
}
