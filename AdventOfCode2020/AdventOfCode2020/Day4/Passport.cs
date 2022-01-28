using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public sealed record Passport()
    {
        private readonly string? _byr;
        private readonly string? _iyr;
        private readonly string? _eyr;
        private readonly string? _hgt;
        private readonly string? _hcl;
        private readonly string? _ecl;
        private readonly string? _pid;
        private string? _cid;

        public Passport(IReadOnlyDictionary<string, string> fields) : this()
        {
            fields.TryGetValue("byr", out _byr);
            fields.TryGetValue("iyr", out _iyr);
            fields.TryGetValue("eyr", out _eyr);
            fields.TryGetValue("hgt", out _hgt);
            fields.TryGetValue("hcl", out _hcl);
            fields.TryGetValue("ecl", out _ecl);
            fields.TryGetValue("pid", out _pid);
            fields.TryGetValue("cid", out _cid);
        }

        public bool ContainsAllRequiredFields()
        {
            return _byr != null && _iyr != null && _eyr != null && _hgt != null && _hcl != null && _ecl != null &&
                   _pid != null;
        }

        public bool AllFieldsAreValid()
        {
            return ByrIsValid() && IyrIsValid() && EyrIsValid() && HgtIsValid() && HclIsValid() && EclIsValid() &&
                   PidIsValid();
        }

        private bool ByrIsValid()
        {
            if (_byr == null)
            {
                return false;
            }

            var num = Convert.ToInt32(_byr);
            return num >= 1920 && num <= 2002;
        }

        private bool IyrIsValid()
        {
            if (_iyr == null)
            {
                return false;
            }

            var num = Convert.ToInt32(_iyr);
            return num >= 2010 && num <= 2020;
        }

        private bool EyrIsValid()
        {
            if (_eyr == null)
            {
                return false;
            }

            var num = Convert.ToInt32(_eyr);
            return num >= 2020 && num <= 2030;
        }

        private bool HgtIsValid()
        {
            if (_hgt == null)
            {
                return false;
            }

            if (!_hgt.Contains("cm") && !_hgt.Contains("in"))
            {
                return false;
            }

            var height = Convert.ToInt32(_hgt.Substring(0, _hgt.Length - 2));
            if (_hgt.Contains("cm"))
            {
                return height >= 150 && height <= 193;
            } 
            
            return height >= 59 && height <= 76;
        }

        private bool HclIsValid()
        {
            var regexp = new Regex("^#([0-9a-f]{6})$");
            return _hcl != null && regexp.IsMatch(_hcl);
        }

        private bool EclIsValid()
        {
            return new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}.Any(x => x == _ecl);
        }

        private bool PidIsValid()
        {
            var regexp = new Regex("^[0-9]{9}$");
            return _pid != null && regexp.IsMatch(_pid);
        }
    }
}