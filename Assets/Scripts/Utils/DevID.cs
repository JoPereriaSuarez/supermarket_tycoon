using System;
using Unity.Collections;

namespace STycoon.Utils
{
	[Serializable]
	public struct DevID
	{
	#if UNITY_EDITOR
		public FixedString32Bytes id;
		
		public override string ToString() => id.ToString();
#endif
	}
}