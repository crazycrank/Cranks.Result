using System.Collections.Generic;

namespace ResultZ
{
    public static class Result
    {
        public static IResult Pass(params IReason[] reasons) => new Passed(reasons);
        public static IResult Pass(IEnumerable<IReason> reasons) => new Passed(reasons);
        public static IResult Pass(string message, params IReason[] reasons) => new Passed(message, reasons);
        public static IResult Pass(string message, IEnumerable<IReason> reasons) => new Passed(message, reasons);

        public static IResult<TValue> Pass<TValue>(TValue value, params IReason[] reasons) => new Passed<TValue>(value, reasons);
        public static IResult<TValue> Pass<TValue>(TValue value, IEnumerable<IReason> reasons) => new Passed<TValue>(value, reasons);
        public static IResult<TValue> Pass<TValue>(TValue value, string message, params IReason[] reasons) => new Passed<TValue>(value, message, reasons);
        public static IResult<TValue> Pass<TValue>(TValue value, string message, IEnumerable<IReason> reasons) => new Passed<TValue>(value, message, reasons);

        public static IResult Fail(params IReason[] reasons) => new Failed(reasons);
        public static IResult Fail(IEnumerable<IReason> reasons) => new Failed(reasons);
        public static IResult Fail(string message, params IReason[] reasons) => new Failed(message, reasons);
        public static IResult Fail(string message, IEnumerable<IReason> reasons) => new Failed(message, reasons);

        public static IResult<TValue> Fail<TValue>(params IReason[] reasons) => new Failed<TValue>(reasons);
        public static IResult<TValue> Fail<TValue>(IEnumerable<IReason> reasons) => new Failed<TValue>(reasons);
        public static IResult<TValue> Fail<TValue>(string message, params IReason[] reasons) => new Failed<TValue>(message, reasons);
        public static IResult<TValue> Fail<TValue>(string message, IEnumerable<IReason> reasons) => new Failed<TValue>(message, reasons);
    }
}
