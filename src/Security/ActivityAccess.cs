using System;

namespace restlessmedia.Module.Security
{
	[Flags]
	public enum ActivityAccess : byte
	{
		/// <summary>
		/// No access set
		/// </summary>
		None = 0,
		/// <summary>
		/// User has basic access to this activity
		/// </summary>
		Basic = 1,
		/// <summary>
		/// User has create privileges on this activity
		/// </summary>
		Create = 2,
		/// <summary>
		/// User has read privileges on this activity
		/// </summary>
		Read = 4,
		/// <summary>
		/// User has update privileges on this activity
		/// </summary>
		Update = 8,
		/// <summary>
		/// User has delete privileges on this activity
		/// </summary>
		Delete = 16,
		/// <summary>
		/// Create, read, update & delete
		/// </summary>
		CRUD = 30
	}
}