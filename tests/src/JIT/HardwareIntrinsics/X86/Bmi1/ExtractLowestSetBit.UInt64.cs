// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\X86\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace JIT.HardwareIntrinsics.X86
{
    public static partial class Program
    {
        private static void ExtractLowestSetBitUInt64()
        {
            var test = new ScalarUnaryOpTest__ExtractLowestSetBitUInt64();

            if (test.IsSupported)
            {
                // Validates basic functionality works, using Unsafe.ReadUnaligned
                test.RunBasicScenario_UnsafeRead();

                // Validates calling via reflection works, using Unsafe.ReadUnaligned
                test.RunReflectionScenario_UnsafeRead();

                // Validates passing a static member works
                test.RunClsVarScenario();

                // Validates passing a local works, using Unsafe.ReadUnaligned
                test.RunLclVarScenario_UnsafeRead();

                // Validates passing the field of a local works
                test.RunLclFldScenario();

                // Validates passing an instance member works
                test.RunFldScenario();
            }
            else
            {
                // Validates we throw on unsupported hardware
                test.RunUnsupportedScenario();
            }

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class ScalarUnaryOpTest__ExtractLowestSetBitUInt64
    {
        private static UInt64 _data;

        private static UInt64 _clsVar;

        private UInt64 _fld;

        static ScalarUnaryOpTest__ExtractLowestSetBitUInt64()
        {
            var random = new Random();
            _clsVar = (ulong)(random.Next(0, int.MaxValue));
        }

        public ScalarUnaryOpTest__ExtractLowestSetBitUInt64()
        {
            Succeeded = true;

            var random = new Random();
            
            _fld = (ulong)(random.Next(0, int.MaxValue));
            _data = (ulong)(random.Next(0, int.MaxValue));
        }

        public bool IsSupported => Bmi1.IsSupported && (Environment.Is64BitProcess || ((typeof(UInt64) != typeof(long)) && (typeof(UInt64) != typeof(ulong))));

        public bool Succeeded { get; set; }

        public void RunBasicScenario_UnsafeRead()
        {
            var result = Bmi1.ExtractLowestSetBit(
                Unsafe.ReadUnaligned<UInt64>(ref Unsafe.As<UInt64, byte>(ref _data))
            );

            ValidateResult(_data, result);
        }

        public void RunReflectionScenario_UnsafeRead()
        {
            var result = typeof(Bmi1).GetMethod(nameof(Bmi1.ExtractLowestSetBit), new Type[] { typeof(UInt64) })
                                     .Invoke(null, new object[] {
                                        Unsafe.ReadUnaligned<UInt64>(ref Unsafe.As<UInt64, byte>(ref _data))
                                     });

            ValidateResult(_data, (UInt64)result);
        }

        public void RunClsVarScenario()
        {
            var result = Bmi1.ExtractLowestSetBit(
                _clsVar
            );

            ValidateResult(_clsVar, result);
        }

        public void RunLclVarScenario_UnsafeRead()
        {
            var data = Unsafe.ReadUnaligned<UInt64>(ref Unsafe.As<UInt64, byte>(ref _data));
            var result = Bmi1.ExtractLowestSetBit(data);

            ValidateResult(data, result);
        }

        public void RunLclFldScenario()
        {
            var test = new ScalarUnaryOpTest__ExtractLowestSetBitUInt64();
            var result = Bmi1.ExtractLowestSetBit(test._fld);

            ValidateResult(test._fld, result);
        }

        public void RunFldScenario()
        {
            var result = Bmi1.ExtractLowestSetBit(_fld);
            ValidateResult(_fld, result);
        }

        public void RunUnsupportedScenario()
        {
            Succeeded = false;

            try
            {
                RunBasicScenario_UnsafeRead();
            }
            catch (PlatformNotSupportedException)
            {
                Succeeded = true;
            }
        }

        private void ValidateResult(UInt64 data, UInt64 result, [CallerMemberName] string method = "")
        {
            var isUnexpectedResult = false;

            isUnexpectedResult = ((unchecked((ulong)(-(long)data)) & data) != result);

            if (isUnexpectedResult)
            {
                Console.WriteLine($"{nameof(Bmi1)}.{nameof(Bmi1.ExtractLowestSetBit)}<UInt64>(UInt64): ExtractLowestSetBit failed:");
                Console.WriteLine($"    data: {data}");
                Console.WriteLine($"  result: {result}");
                Console.WriteLine();
            }
        }
    }
}