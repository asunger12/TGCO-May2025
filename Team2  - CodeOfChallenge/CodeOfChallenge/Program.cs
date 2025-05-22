using System;
using System.Collections.Generic;

class RobotGrid
{
    public static void Main()
    {
        // List of coordinates to check for safety
        var coordinates = new (int x, int y)[]
        {
            (3, 4),
            (6, 9),
            (96, -69),
            (67, 43),
            (123, 456)
        };

        // Checking each coordinate and printing if it's safe or mined
        foreach (var (x, y) in coordinates)
        {
            string status = isSafe(x, y) ? "Safe" : "Mined";
            Console.WriteLine($"({x}, {y}) -> {status}");
        }

        // Example: Calculate the total number of safe squares reachable from (0, 0)
        Console.WriteLine($"Total safe squares reachable from (0, 0): {totalSafeSquares()}");

        // Example: Calculate the shortest safe journey from (0, 0) to (5, 5)
        int result = shortestSafeJourney(0, 0, 5, 5);
        Console.WriteLine(result != -1 ? $"Shortest safe journey: {result} steps" : "No safe path found");
    }

    // Method to check if a coordinate is safe to visit
    static bool isSafe(int x, int y)
    {
        // If either x or y is zero, the product is zero → sum of digits = 0, so safe
        if (x == 0 || y == 0)
            return true;

        // Calculate the product of x and y
        int product = x * y;

        // Calculate the sum of digits of the product
        int digitSum = SumOfDigits(Math.Abs(product)); // Use absolute value to handle negative products

        // Return true if sum of digits < 19, false otherwise
        return digitSum < 19;
    }

    // Method to sum the digits of a number
    static int SumOfDigits(int n)
    {
        int sum = 0;
        while (n > 0)
        {
            sum += n % 10;
            n /= 10;
        }
        return sum;
    }

    // Optimized method to calculate the total number of safe squares the robot can reach from (0, 0)
    static int totalSafeSquares()
    {
        // Define boundaries for search
        int MAX_X = -9999; // limit for x-coordinate
        int MAX_Y = 9999; // limit for y-coordinate

        // Set to track visited positions
        var visited = new HashSet<(int, int)>();
        var queue = new Queue<(int, int)>();

        // Start at (0, 0)
        queue.Enqueue((0, 0));
        visited.Add((0, 0));

        // Directions for up, down, left, right moves
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        int safeSquares = 0;

        // Perform BFS, but limit search to defined bounds
        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();
            safeSquares++;

            // Explore 4 possible directions (up, down, left, right)
            for (int i = 0; i < 4; i++)
            {
                int newX = x + dx[i];
                int newY = y + dy[i];

                // Check if the new position is within bounds, not visited, and safe
                if (Math.Abs(newX) <= MAX_X && Math.Abs(newY) <= MAX_Y && !visited.Contains((newX, newY)) && isSafe(newX, newY))
                {
                    visited.Add((newX, newY));
                    queue.Enqueue((newX, newY));
                }
            }
        }

        return safeSquares;
    }

    // Method to find the shortest safe journey from (a, b) to (x, y)
    static int shortestSafeJourney(int a, int b, int x, int y)
    {
        // If the start or destination is not safe, return -1 immediately
        if (!isSafe(a, b) || !isSafe(x, y))
            return -1;

        var visited = new HashSet<(int, int)>();
        var queue = new Queue<(int, int, int)>();  // (x, y, distance)
        queue.Enqueue((a, b, 0));
        visited.Add((a, b));

        // Directions for up, down, left, right moves
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        // Perform BFS to find the shortest path
        while (queue.Count > 0)
        {
            var (currentX, currentY, distance) = queue.Dequeue();

            // If we've reached the destination, return the distance
            if (currentX == x && currentY == y)
                return distance;

            // Explore 4 possible directions (up, down, left, right)
            for (int i = 0; i < 4; i++)
            {
                int newX = currentX + dx[i];
                int newY = currentY + dy[i];

                // Check if the new position is within bounds, not visited, and safe
                if (!visited.Contains((newX, newY)) && isSafe(newX, newY))
                {
                    visited.Add((newX, newY));
                    queue.Enqueue((newX, newY, distance + 1));
                }
            }
        }

        // If we exhaust the queue without finding a safe path, return -1
        return -1;
    }
}
