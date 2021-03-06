﻿using System;
using System.Text;
using ExpectedObjects.Reporting;

namespace ExpectedObjects
{
    public static class ShouldExtensions
    {
        public static void ShouldEqual<T>(this ExpectedObject expected, T actual)
        {
            IWriter writer = new ShouldWriter();

            if (!expected.Equals(actual, writer))
            {
                var results = writer.GetFormattedResults();

                if (!string.IsNullOrEmpty(results))
                    throw new ComparisonException(results);
            }
        }

        public static void ShouldNotEqual<T>(this ExpectedObject expected, T actual)
        {
            var writer = new ShouldWriter();

            if (expected.Equals(actual, writer))
            {
                var sb = new StringBuilder();
                sb.Append($"The expected object should not equal the actual object.");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(writer.GetTrunkFormattedResults());
                throw new ComparisonException(sb.ToString());
            }
        }

        public static void ShouldMatch<T>(this ExpectedObject expected, T actual)
        {
            IWriter writer = new ShouldWriter();

            if (!expected.Equals(actual, writer, true))
            {
                var results = writer.GetFormattedResults();
                throw new ComparisonException(results);
            }
        }

        public static void ShouldNotMatch<T>(this ExpectedObject expected, T actual)
        {
            var writer = new ShouldWriter();

            if (expected.Equals(actual, writer, true))
            {
                var sb = new StringBuilder();
                sb.Append($"The expected object should not match the actual object.");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(writer.GetTrunkFormattedResults());
                throw new ComparisonException(sb.ToString());
            }
        }
    }
}