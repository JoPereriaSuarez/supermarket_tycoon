using System;
using Unity.Collections;

namespace Utils
{
	[Serializable]
	public struct DevID
	{
	#if UNITY_EDITOR
		public FixedString32Bytes id;
	#endif
	}
}