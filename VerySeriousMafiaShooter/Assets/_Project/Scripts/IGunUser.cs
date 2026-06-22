using System;

public interface IGunUser
{
    public event EventHandler OnReloadGun;
    public event BoolReturnEventDelegate OnFireGun;
    public delegate bool BoolReturnEventDelegate(object sender, EventArgs e);
}
