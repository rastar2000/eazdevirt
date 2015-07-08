﻿using System;
using System.Collections.Generic;
using System.Linq;
using de4dot.blocks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using eazdevirt.Util;

namespace eazdevirt
{
	/// <summary>
	/// Extensions for detecting original instruction type (opcode).
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Attempt to identify a virtual instruction with its original CIL opcode.
		/// </summary>
		/// <param name="ins">Virtual instruction</param>
		/// <exception cref="OriginalOpcodeUnknownException">Thrown if unable to identify original CIL opcode</exception>
		/// <remarks>What this method does could probably be better done through reflection/attributes</remarks>
		/// <returns>CIL opcode</returns>
		public static Code Identify(this EazVirtualInstruction ins)
		{
			if (ins.Is_Add())
				return Code.Add;
			else if (ins.Is_Add_Ovf())
				return Code.Add_Ovf;
			else if (ins.Is_Add_Ovf_Un())
				return Code.Add_Ovf_Un;
			else if (ins.Is_And())
				return Code.And;
			else if (ins.Is_Bge())
				return Code.Bge;
			else if (ins.Is_Blt())
				return Code.Blt;
			else if (ins.Is_Call())
				return Code.Call;
			else if (ins.Is_Cgt())
				return Code.Cgt;
			else if (ins.Is_Ckfinite())
				return Code.Ckfinite;
			else if (ins.Is_Clt())
				return Code.Clt;
			else if (ins.Is_Div())
				return Code.Div;
			else if (ins.Is_Div_Un())
				return Code.Div_Un;
			else if (ins.Is_Ldarg())
				return Code.Ldarg;
			else if (ins.Is_Ldarg_S())
				return Code.Ldarg_S;
			else if (ins.Is_Ldarga())
				return Code.Ldarga;
			else if (ins.Is_Ldarga_S())
				return Code.Ldarga_S;
			else if (ins.Is_Ldarg_0())
				return Code.Ldarg_0;
			else if (ins.Is_Ldarg_1())
				return Code.Ldarg_1;
			else if (ins.Is_Ldarg_2())
				return Code.Ldarg_2;
			else if (ins.Is_Ldarg_3())
				return Code.Ldarg_3;
			else if (ins.Is_Ldc_I4())
				return Code.Ldc_I4;
			else if (ins.Is_Ldc_I4_S())
				return Code.Ldc_I4_S;
			else if (ins.Is_Ldc_I4_0())
				return Code.Ldc_I4_0;
			else if (ins.Is_Ldc_I4_1())
				return Code.Ldc_I4_1;
			else if (ins.Is_Ldc_I4_2())
				return Code.Ldc_I4_2;
			else if (ins.Is_Ldc_I4_3())
				return Code.Ldc_I4_3;
			else if (ins.Is_Ldc_I4_4())
				return Code.Ldc_I4_4;
			else if (ins.Is_Ldc_I4_5())
				return Code.Ldc_I4_5;
			else if (ins.Is_Ldc_I4_6())
				return Code.Ldc_I4_6;
			else if (ins.Is_Ldc_I4_7())
				return Code.Ldc_I4_7;
			else if (ins.Is_Ldc_I4_8())
				return Code.Ldc_I4_8;
			else if (ins.Is_Ldc_I4_M1())
				return Code.Ldc_I4_M1;
			else if (ins.Is_Ldc_I8())
				return Code.Ldc_I8;
			else if (ins.Is_Ldc_R4())
				return Code.Ldc_R4;
			else if (ins.Is_Ldc_R8())
				return Code.Ldc_R8;
			else if (ins.Is_Ldloc())
				return Code.Ldloc;
			else if (ins.Is_Ldloc_S())
				return Code.Ldloc_S;
			else if (ins.Is_Ldloc_0())
				return Code.Ldloc_0;
			else if (ins.Is_Ldloc_1())
				return Code.Ldloc_1;
			else if (ins.Is_Ldloc_2())
				return Code.Ldloc_2;
			else if (ins.Is_Ldloc_3())
				return Code.Ldloc_3;
			else if (ins.Is_Ldnull())
				return Code.Ldnull;
			else if (ins.Is_Ldstr())
				return Code.Ldstr;
			else if (ins.Is_Or())
				return Code.Or;
			else if (ins.Is_Rem())
				return Code.Rem;
			else if (ins.Is_Rem_Un())
				return Code.Rem_Un;
			else if (ins.Is_Ret())
				return Code.Ret;
			else if (ins.Is_Rethrow())
				return Code.Rethrow;
			else if (ins.Is_Shl())
				return Code.Shl;
			else if (ins.Is_Shr())
				return Code.Shr;
			else if (ins.Is_Shr_Un())
				return Code.Shr_Un;
			else if (ins.Is_Starg())
				return Code.Starg;
			else if (ins.Is_Starg_S())
				return Code.Starg_S;
			else if (ins.Is_Stloc())
				return Code.Stloc;
			else if (ins.Is_Stloc_S())
				return Code.Stloc_S;
			else if (ins.Is_Stloc_0())
				return Code.Stloc_0;
			else if (ins.Is_Stloc_1())
				return Code.Stloc_1;
			else if (ins.Is_Stloc_2())
				return Code.Stloc_2;
			else if (ins.Is_Stloc_3())
				return Code.Stloc_3;
			else if (ins.Is_Sub())
				return Code.Sub;
			else if (ins.Is_Sub_Ovf())
				return Code.Sub_Ovf;
			else if (ins.Is_Sub_Ovf_Un())
				return Code.Sub_Ovf_Un;
			else if (ins.Is_Throw())
				return Code.Throw;
			else if (ins.Is_Xor())
				return Code.Xor;

			throw new OriginalOpcodeUnknownException(ins);
		}

		public static Boolean TryIdentify(this EazVirtualInstruction ins, out Code code)
		{
			try
			{
				code = ins.Identify();
				return true;
			}
			catch (OriginalOpcodeUnknownException)
			{
				code = Code.UNKNOWN2;
				return false;
			}
		}

		public static Boolean Is_And(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirect(
				new Code[] { Code.Ldloc_S, Code.Ldloc_S, Code.And, Code.Callvirt, Code.Ldloc_0, Code.Ret }
			);
		}

		public static Boolean Is_Xor(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirect(
				new Code[] { Code.Ldloc_S, Code.Ldloc_S, Code.Xor, Code.Callvirt, Code.Ldloc_0, Code.Ret }
			);
		}

		public static Boolean Is_Shl(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirect(
				new Code[] { Code.Ldloc_S, Code.Ldloc_S, Code.Ldc_I4_S, Code.And, Code.Shl, Code.Stloc_S,
				Code.Newobj, Code.Stloc_0, Code.Ldloc_0, Code.Ldloc_S, Code.Callvirt, Code.Ldloc_0, Code.Ret }
			);
		}

		/// <summary>
		/// OpCode pattern seen in the Shr_* helper method.
		/// </summary>
		private static readonly Code[] Pattern_Shr = new Code[] {
			Code.Ldc_I4_S, Code.And, Code.Shr, Code.Callvirt, Code.Ldloc_0, Code.Ret
		};

		public static Boolean Is_Shr(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirectWithBoolean(true, Pattern_Shr);
		}

		public static Boolean Is_Shr_Un(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirectWithBoolean(false, Pattern_Shr);
		}

		/// <summary>
		/// OpCode pattern seen in the Sub_* helper method.
		/// </summary>
		private static readonly Code[] Pattern_Sub = new Code[] {
			Code.Ldloc_0, Code.Ldloc_1, Code.Sub, Code.Stloc_2, Code.Newobj, Code.Stloc_3,
			Code.Ldloc_3, Code.Ldloc_2, Code.Callvirt, Code.Ldloc_3, Code.Ret
		};

		public static Boolean Is_Sub(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirectWithBoolean2(false, false, Pattern_Sub);
		}

		public static Boolean Is_Sub_Ovf(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirectWithBoolean2(true, false, Pattern_Sub);
		}

		public static Boolean Is_Sub_Ovf_Un(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirectWithBoolean2(true, true, Pattern_Sub);
		}

		/// <summary>
		/// OpCode pattern seen in the Add_* helper method.
		/// </summary>
		private static readonly Code[] Pattern_Add = new Code[] {
			Code.Ldloc_0, Code.Ldloc_1, Code.Add, Code.Stloc_2, Code.Newobj, Code.Stloc_3,
			Code.Ldloc_3, Code.Ldloc_2, Code.Callvirt, Code.Ldloc_3, Code.Ret
		};

		public static Boolean Is_Add(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirectWithBoolean2(false, false, Pattern_Add);
		}

		public static Boolean Is_Add_Ovf(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirectWithBoolean2(true, false, Pattern_Add);
		}

		public static Boolean Is_Add_Ovf_Un(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirectWithBoolean2(true, true, Pattern_Add);
		}

		/// <summary>
		/// OpCode pattern seen in the Less-Than helper method.
		/// Used in: Clt, Blt, Bge (negated)
		/// </summary>
		private static readonly Code[] Pattern_Clt = new Code[] {
			Code.Ldloc_S, Code.Ldloc_S, Code.Blt_S,
			Code.Ldloc_S, Code.Call, Code.Brtrue_S, // System.Double::IsNaN(float64)
			Code.Ldloc_S, Code.Call, Code.Br_S      // System.Double::IsNaN(float64)
		};

		public static Boolean Is_Clt(this EazVirtualInstruction ins)
		{
			return ins.Matches(new Code[] {
				Code.Call, Code.Brtrue_S, Code.Ldc_I4_0, Code.Br_S, Code.Ldc_I4_1,
				Code.Callvirt, Code.Ldloc_2, Code.Call, Code.Ret
			}) && ins.MatchesIndirect(Pattern_Clt);
		}

		public static Boolean Is_Bge(this EazVirtualInstruction ins)
		{
			return ins.Matches(new Code[] {
				Code.Call, Code.Brtrue_S, Code.Ldarg_1, Code.Castclass
			}) && ins.MatchesIndirect(Pattern_Clt);
		}

		public static Boolean Is_Blt(this EazVirtualInstruction ins)
		{
			return ins.Matches(new Code[] {
				Code.Call, Code.Brfalse_S, Code.Ldarg_1, Code.Castclass
			}) && ins.MatchesIndirect(Pattern_Clt);
		}

		/// <summary>
		/// OpCode pattern seen in the Div_* helper method.
		/// </summary>
		private static readonly Code[] Pattern_Div = new Code[] {
			Code.Ldloc_S, Code.Ldloc_S, Code.Div, Code.Callvirt, Code.Ldloc_0, Code.Ret
		};

		public static Boolean Is_Div(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirectWithBoolean(false, Pattern_Div);
		}

		public static Boolean Is_Div_Un(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirectWithBoolean(true, Pattern_Div);
		}

		/// <summary>
		/// OpCode pattern seen in the Greater-Than helper method.
		/// Used in: Cgt
		/// </summary>
		/// <remarks>Greater-than for Double, Int32, Int64 but not-equal for other?</remarks>
		private static readonly Code[] Pattern_Cgt = new Code[] {
			Code.Ldarg_0, Code.Castclass, Code.Callvirt, Code.Ldarg_1,
			Code.Castclass, Code.Callvirt, Code.Cgt_Un, Code.Stloc_0
		};

		/// <remarks>Unsure</remarks>
		public static Boolean Is_Cgt(this EazVirtualInstruction ins)
		{
			return ins.Matches(new Code[] {
				Code.Call, Code.Brtrue_S, Code.Ldc_I4_0, Code.Br_S, Code.Ldc_I4_1,
				Code.Callvirt, Code.Ldloc_2, Code.Call, Code.Ret
			}) && ins.MatchesIndirect(Pattern_Cgt);
		}

		/// <summary>
		/// OpCode pattern seen in the Rem_* helper method.
		/// </summary>
		private static readonly Code[] Pattern_Rem = new Code[] {
			Code.Ldloc_S, Code.Ldloc_S, Code.Rem, Code.Callvirt, Code.Ldloc_0, Code.Ret
		};

		public static Boolean Is_Rem(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirectWithBoolean(false, Pattern_Rem);
		}

		public static Boolean Is_Rem_Un(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirectWithBoolean(true, Pattern_Rem);
		}

		public static Boolean Is_Or(this EazVirtualInstruction ins)
		{
			return ins.MatchesIndirect(new Code[] {
				Code.Ldloc_S, Code.Ldloc_S, Code.Or, Code.Callvirt, Code.Ldloc_0, Code.Ret
			});
		}

		/// <summary>
		/// OpCode pattern seen in the Starg_* delegate methods.
		/// </summary>
		private static readonly Code[] Pattern_Starg = new Code[] {
			Code.Ldarg_0, Code.Ldfld, Code.Ldloc_0, Code.Callvirt, Code.Ldelem
		};

		/// <summary>
		/// OpCode pattern seen at the "tail" of the Starg_* delegate methods.
		/// </summary>
		/// <remarks>
		/// Without this tail check, multiple delegate methods were being associated
		/// with both Starg and Starg_S
		/// </remarks>
		private static readonly Code[] Pattern_Tail_Starg = new Code[] {
			Code.Callvirt, Code.Pop, Code.Ret
		};

		public static Boolean Is_Starg(this EazVirtualInstruction ins)
		{
			var sub = ins.Find(Pattern_Starg);
			return sub != null && sub[3].Operand is MethodDef
				&& ((MethodDef)sub[3].Operand).ReturnType.FullName.Equals("System.UInt16")
				&& ins.Matches(Pattern_Tail_Starg);
		}

		public static Boolean Is_Starg_S(this EazVirtualInstruction ins)
		{
			var sub = ins.Find(Pattern_Starg);
			return sub != null && sub[3].Operand is MethodDef
				&& ((MethodDef)sub[3].Operand).ReturnType.FullName.Equals("System.Byte")
				&& ins.Matches(Pattern_Tail_Starg);
		}

		/// <summary>
		/// OpCode pattern seen in the Ldarg_C delegate methods (Ldarg_0, Ldarg_1, Ldarg_2, Ldarg_3).
		/// </summary>
		private static readonly Code[] Pattern_Ldarg_C = new Code[] {
			Code.Ldarg_0, Code.Ldarg_0, Code.Ldfld, Code.Ldc_I4, Code.Ldelem, // Code.Ldc_I4 changes depending on _C
			Code.Callvirt, Code.Call, Code.Ret
		};

		private static Boolean Is_Ldarg_C(EazVirtualInstruction ins, Code code)
		{
			// Ldarg_C delegates will reference the arguments field in their Ldfld, which sets them apart from
			// other very similar delegates
			return ins.Matches(ins.ModifyPattern(Pattern_Ldarg_C, Code.Ldc_I4, code))
				&& ((FieldDef)ins.DelegateMethod.Body.Instructions[2].Operand).MDToken == ins.Virtualization.ArgumentsField.MDToken;
		}

		public static Boolean Is_Ldarg_0(this EazVirtualInstruction ins)
		{
			return Is_Ldarg_C(ins, Code.Ldc_I4_0);
		}

		public static Boolean Is_Ldarg_1(this EazVirtualInstruction ins)
		{
			return Is_Ldarg_C(ins, Code.Ldc_I4_1);
		}

		public static Boolean Is_Ldarg_2(this EazVirtualInstruction ins)
		{
			return Is_Ldarg_C(ins, Code.Ldc_I4_2);
		}

		public static Boolean Is_Ldarg_3(this EazVirtualInstruction ins)
		{
			return Is_Ldarg_C(ins, Code.Ldc_I4_3);
		}

		/// <summary>
		/// OpCode pattern seen in the Ldarga, Ldarga_S delegate methods.
		/// </summary>
		private static readonly Code[] Pattern_Ldarga = new Code[] {
			Code.Ldloc_1, Code.Ldarg_0, Code.Ldfld, Code.Ldloc_0, Code.Callvirt,
			Code.Ldelem, Code.Callvirt, Code.Ldloc_1, Code.Call, Code.Ret
		};

		/// <remarks>Unsure</remarks>
		public static Boolean Is_Ldarga(this EazVirtualInstruction ins)
		{
			var sub = ins.Find(Pattern_Ldarga);
			return sub != null
				&& ((FieldDef)sub[2].Operand).MDToken == ins.Virtualization.ArgumentsField.MDToken
				&& ((MethodDef)sub[4].Operand).ReturnType.FullName.Equals("System.UInt16");
		}

		/// <remarks>Unsure</remarks>
		public static Boolean Is_Ldarga_S(this EazVirtualInstruction ins)
		{
			var sub = ins.Find(Pattern_Ldarga);
			return sub != null
				&& ((FieldDef)sub[2].Operand).MDToken == ins.Virtualization.ArgumentsField.MDToken
				&& ((MethodDef)sub[4].Operand).ReturnType.FullName.Equals("System.Byte");
		}

		/// <summary>
		/// OpCode pattern seen in the Ldarg, Ldarg_S delegate methods.
		/// </summary>
		/// <remarks>There are other delegate methods that match this exact pattern.</remarks>
		private static readonly Code[] Pattern_Ldarg = new Code[] {
			Code.Ldarg_1, Code.Castclass, Code.Stloc_0, Code.Ldarg_0, Code.Ldarg_0,
			Code.Ldfld, Code.Ldloc_0, Code.Callvirt, Code.Ldelem, Code.Callvirt,
			Code.Call, Code.Ret
		};

		public static Boolean Is_Ldarg(this EazVirtualInstruction ins)
		{
			return ins.MatchesEntire(Pattern_Ldarg)
				&& ((MethodDef)ins.DelegateMethod.Body.Instructions[7].Operand)
				   .ReturnType.FullName.Equals("System.UInt16")
				&& ((FieldDef)ins.DelegateMethod.Body.Instructions[5].Operand)
				   .MDToken == ins.Virtualization.ArgumentsField.MDToken;
		}

		public static Boolean Is_Ldarg_S(this EazVirtualInstruction ins)
		{
			return ins.MatchesEntire(Pattern_Ldarg)
				&& ((MethodDef)ins.DelegateMethod.Body.Instructions[7].Operand)
				   .ReturnType.FullName.Equals("System.Byte")
				&& ((FieldDef)ins.DelegateMethod.Body.Instructions[5].Operand)
				   .MDToken == ins.Virtualization.ArgumentsField.MDToken;
		}

		/// <summary>
		/// OpCode pattern seen in the Ldc_I4_C delegate methods.
		/// </summary>
		private static readonly Code[] Pattern_Ldc_I4_C = new Code[] {
			Code.Ldarg_0, Code.Newobj, Code.Stloc_0, Code.Ldloc_0, Code.Ldc_I4, // Code.Ldc_I4 changes depending on _C
			Code.Callvirt, Code.Ldloc_0, Code.Call, Code.Ret
		};

		private static Boolean Is_Ldc_I4_C(EazVirtualInstruction ins, Code code)
		{
			return ins.MatchesEntire(ins.ModifyPattern(Pattern_Ldc_I4_C, Code.Ldc_I4, code));
		}

		public static Boolean Is_Ldc_I4_0(this EazVirtualInstruction ins)
		{
			return Is_Ldc_I4_C(ins, Code.Ldc_I4_0);
		}

		public static Boolean Is_Ldc_I4_1(this EazVirtualInstruction ins)
		{
			return Is_Ldc_I4_C(ins, Code.Ldc_I4_1);
		}

		public static Boolean Is_Ldc_I4_2(this EazVirtualInstruction ins)
		{
			return Is_Ldc_I4_C(ins, Code.Ldc_I4_2);
		}

		public static Boolean Is_Ldc_I4_3(this EazVirtualInstruction ins)
		{
			return Is_Ldc_I4_C(ins, Code.Ldc_I4_3);
		}

		public static Boolean Is_Ldc_I4_4(this EazVirtualInstruction ins)
		{
			return Is_Ldc_I4_C(ins, Code.Ldc_I4_4);
		}

		public static Boolean Is_Ldc_I4_5(this EazVirtualInstruction ins)
		{
			return Is_Ldc_I4_C(ins, Code.Ldc_I4_5);
		}

		public static Boolean Is_Ldc_I4_6(this EazVirtualInstruction ins)
		{
			return Is_Ldc_I4_C(ins, Code.Ldc_I4_6);
		}

		public static Boolean Is_Ldc_I4_7(this EazVirtualInstruction ins)
		{
			return Is_Ldc_I4_C(ins, Code.Ldc_I4_7);
		}

		public static Boolean Is_Ldc_I4_8(this EazVirtualInstruction ins)
		{
			return Is_Ldc_I4_C(ins, Code.Ldc_I4_8);
		}

		public static Boolean Is_Ldc_I4_M1(this EazVirtualInstruction ins)
		{
			return Is_Ldc_I4_C(ins, Code.Ldc_I4_M1);
		}

		/// <summary>
		/// OpCode pattern seen in the Ldc_I4, Ldc_I4_S, Ldc_I8, Ldc_R4, Ldc_R8 delegate methods.
		/// </summary>
		public static readonly Code[] Pattern_Ldc = new Code[] {
			Code.Ldarg_0, Code.Ldarg_1, Code.Call, Code.Ret
		};

		public static Boolean _Is_Ldc(EazVirtualInstruction ins, OperandType expectedOperandType)
		{
			OperandType operandType;
			return ins.MatchesEntire(Pattern_Ldc)
				&& ins.TryGetOperandType(out operandType)
				&& operandType == expectedOperandType;
		}

		public static Boolean Is_Ldc_I4(this EazVirtualInstruction ins)
		{
			return _Is_Ldc(ins, OperandType.InlineI);
		}

		public static Boolean Is_Ldc_I4_S(this EazVirtualInstruction ins)
		{
			return _Is_Ldc(ins, OperandType.ShortInlineI);
		}

		public static Boolean Is_Ldc_I8(this EazVirtualInstruction ins)
		{
			return _Is_Ldc(ins, OperandType.InlineI8);
		}

		public static Boolean Is_Ldc_R4(this EazVirtualInstruction ins)
		{
			return _Is_Ldc(ins, OperandType.ShortInlineR);
		}

		public static Boolean Is_Ldc_R8(this EazVirtualInstruction ins)
		{
			return _Is_Ldc(ins, OperandType.InlineR);
		}

		/// <summary>
		/// OpCode pattern seen in the Ldloc_C delegate methods.
		/// </summary>
		private static readonly Code[] Pattern_Ldloc_C = new Code[] {
			Code.Ldarg_0, Code.Ldarg_0, Code.Ldfld, Code.Ldc_I4, Code.Ldelem, // Code.Ldc_I4 changes depending on _C
			Code.Callvirt, Code.Call, Code.Ret
		};

		private static Boolean Is_Ldloc_C(EazVirtualInstruction ins, Code code)
		{
			return ins.MatchesEntire(ins.ModifyPattern(Pattern_Ldloc_C, Code.Ldc_I4, code))
				&& ((FieldDef)ins.DelegateMethod.Body.Instructions[2].Operand).MDToken == ins.Virtualization.LocalsField.MDToken;
		}

		public static Boolean Is_Ldloc_0(this EazVirtualInstruction ins)
		{
			return Is_Ldloc_C(ins, Code.Ldc_I4_0);
		}

		public static Boolean Is_Ldloc_1(this EazVirtualInstruction ins)
		{
			return Is_Ldloc_C(ins, Code.Ldc_I4_1);
		}

		public static Boolean Is_Ldloc_2(this EazVirtualInstruction ins)
		{
			return Is_Ldloc_C(ins, Code.Ldc_I4_2);
		}

		public static Boolean Is_Ldloc_3(this EazVirtualInstruction ins)
		{
			return Is_Ldloc_C(ins, Code.Ldc_I4_3);
		}

		/// <summary>
		/// OpCode pattern seen in the Ldloc, Ldloc_S delegate methods.
		/// </summary>
		private static readonly Code[] Pattern_Ldloc = new Code[] {
			Code.Ldarg_1, Code.Castclass, Code.Stloc_0, Code.Ldarg_0, Code.Ldarg_0,
			Code.Ldfld, Code.Ldloc_0, Code.Callvirt, Code.Ldelem, Code.Callvirt,
			Code.Call, Code.Ret
		};

		public static Boolean Is_Ldloc(this EazVirtualInstruction ins)
		{
			return ins.MatchesEntire(Pattern_Ldloc)
				&& ((MethodDef)ins.DelegateMethod.Body.Instructions[7].Operand)
				   .ReturnType.FullName.Equals("System.UInt16");
		}

		public static Boolean Is_Ldloc_S(this EazVirtualInstruction ins)
		{
			return ins.MatchesEntire(Pattern_Ldloc)
				&& ((MethodDef)ins.DelegateMethod.Body.Instructions[7].Operand)
				   .ReturnType.FullName.Equals("System.Byte");
		}

		/// <summary>
		/// OpCode pattern seen in the Stloc_C delegate methods.
		/// </summary>
		private static readonly Code[] Pattern_Stloc_C = new Code[] {
			Code.Ldarg_0, Code.Ldc_I4, Code.Call, Code.Ret // Code.Ldc_I4 changes depending on _C
		};

		/// <summary>
		/// OpCode pattern seen in the Stloc_C helper method.
		/// </summary>
		/// <remarks>Found at the head of the method body</remarks>
		private static readonly Code[] Pattern_Helper_Stloc_C = new Code[] {
			Code.Ldarg_0, Code.Call, Code.Stloc_0, Code.Ldloc_0, Code.Isinst,
		};

		private static Boolean Is_Stloc_C(EazVirtualInstruction ins, Code code)
		{
			return ins.MatchesEntire(ins.ModifyPattern(Pattern_Stloc_C, Code.Ldc_I4, code))
				&& Helpers.FindOpCodePatterns( // Check called method against Pattern_Helper_Stloc_C
					 ((MethodDef)ins.DelegateMethod.Body.Instructions[2].Operand).Body.Instructions,
					 Pattern_Helper_Stloc_C
				   ).Count > 0;
		}

		public static Boolean Is_Stloc_0(this EazVirtualInstruction ins)
		{
			return Is_Stloc_C(ins, Code.Ldc_I4_0);
		}

		public static Boolean Is_Stloc_1(this EazVirtualInstruction ins)
		{
			return Is_Stloc_C(ins, Code.Ldc_I4_1);
		}

		public static Boolean Is_Stloc_2(this EazVirtualInstruction ins)
		{
			return Is_Stloc_C(ins, Code.Ldc_I4_2);
		}

		public static Boolean Is_Stloc_3(this EazVirtualInstruction ins)
		{
			return Is_Stloc_C(ins, Code.Ldc_I4_3);
		}

		/// <summary>
		/// OpCode pattern seen in the Stloc, Stloc_S delegate methods.
		/// </summary>
		private static readonly Code[] Pattern_Stloc = new Code[] {
			Code.Ldarg_1, Code.Castclass, Code.Stloc_0, Code.Ldarg_0, Code.Ldloc_0,
			Code.Callvirt, Code.Call, Code.Ret
		};

		private static Boolean _Is_Stloc(EazVirtualInstruction ins, String indexTypeName)
		{
			return ins.MatchesEntire(Pattern_Stloc)
				&& ((MethodDef)ins.DelegateMethod.Body.Instructions[5].Operand)
				   .ReturnType.FullName.Equals(indexTypeName)
				&& Helpers.FindOpCodePatterns( // Check called method against Pattern_Helper_Stloc_C
					 ((MethodDef)ins.DelegateMethod.Body.Instructions[6].Operand).Body.Instructions,
					 Pattern_Helper_Stloc_C
				   ).Count > 0;
		}

		public static Boolean Is_Stloc(this EazVirtualInstruction ins)
		{
			return _Is_Stloc(ins, "System.UInt16");
		}

		public static Boolean Is_Stloc_S(this EazVirtualInstruction ins)
		{
			return _Is_Stloc(ins, "System.Byte");
		}

		/// <summary>
		/// OpCode pattern seen in the Throw, Rethrow helper methods.
		/// </summary>
		public static readonly Code[] Pattern_Throw = new Code[] {
			Code.Ldarg_0, Code.Isinst, Code.Stloc_0, Code.Ldloc_0,
			Code.Call, Code.Ldarg_0, Code.Call, Code.Ret
		};

		public static Boolean _Is_Throw(EazVirtualInstruction ins, MethodDef helper)
		{
			var matches = Helpers.FindOpCodePatterns(helper.Body.Instructions, Pattern_Throw);
			return matches.Count == 1 && matches[0].Length == Pattern_Throw.Length;
		}

		/// <remarks>Unsure</remarks>
		public static Boolean Is_Throw(this EazVirtualInstruction ins)
		{
			return ins.MatchesEntire(new Code[] {
				Code.Ldarg_0, Code.Call, Code.Stloc_0, Code.Ldloc_0,
				Code.Callvirt, Code.Call, Code.Ret
			}) && _Is_Throw(ins, ((MethodDef)ins.DelegateMethod.Body.Instructions[5].Operand));
		}

		/// <remarks>Unsure</remarks>
		public static Boolean Is_Rethrow(this EazVirtualInstruction ins)
		{
			var sub = ins.Find(new Code[] {
				Code.Newobj, Code.Throw, Code.Ldarg_0, Code.Ldarg_0, Code.Ldfld,
				Code.Callvirt, Code.Callvirt, Code.Stfld, Code.Ldarg_0, Code.Ldfld,
				Code.Call, Code.Ret
			});
			return sub != null && _Is_Throw(ins, ((MethodDef)sub[10].Operand));
		}

		public static Boolean Is_Ckfinite(this EazVirtualInstruction ins)
		{
			var sub = ins.Find(new Code[] {
				Code.Ldloc_0, Code.Callvirt, Code.Call, Code.Brtrue_S,
				Code.Ldloc_0, Code.Callvirt, Code.Call, Code.Brfalse_S,
				Code.Ldstr, Code.Newobj, Code.Throw
			});

			return sub != null
				&& ((IMethod)sub[2].Operand).FullName.Contains("System.Double::IsNaN")
				&& ((IMethod)sub[6].Operand).FullName.Contains("System.Double::IsInfinity");
		}

		public static Boolean Is_Call(this EazVirtualInstruction ins)
		{
			return ins.MatchesEntire(new Code[] {
				Code.Ldarg_1, Code.Castclass, Code.Stloc_0, Code.Ldarg_0, Code.Ldloc_0,
				Code.Callvirt, Code.Call, Code.Stloc_1, Code.Ldarg_0, Code.Ldloc_1,
				Code.Ldc_I4_0, Code.Call, Code.Ret
			});
		}

		public static Boolean Is_Ldstr(this EazVirtualInstruction ins)
		{
			return ins.MatchesEntire(new Code[] {
				Code.Ldarg_1, Code.Castclass, Code.Callvirt, Code.Stloc_0, Code.Ldarg_0,
				Code.Ldloc_0, Code.Call, Code.Stloc_1, Code.Ldarg_0, Code.Newobj,
				Code.Stloc_2, Code.Ldloc_2, Code.Ldloc_1, Code.Callvirt, Code.Ldloc_2,
				Code.Call, Code.Ret
			}) && ((MethodDef)ins.DelegateMethod.Body.Instructions[6].Operand)
			      .ReturnType.FullName.Equals("System.String");
		}

		public static Boolean Is_Ldnull(this EazVirtualInstruction ins)
		{
			return ins.MatchesEntire(new Code[] {
				Code.Ldarg_0, Code.Newobj, Code.Call, Code.Ret
			});
		}

		public static Boolean Is_Ret(this EazVirtualInstruction ins)
		{
			return ins.MatchesEntire(new Code[] {
				Code.Ldarg_0, Code.Call, Code.Ret
			}) && ((MethodDef)ins.DelegateMethod.Body.Instructions[1].Operand).MatchesEntire(new Code[] {
				Code.Ldarg_0, Code.Ldc_I4_1, Code.Stfld, Code.Ret
			});
		}
	}
}