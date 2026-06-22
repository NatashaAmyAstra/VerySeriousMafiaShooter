using System;

public interface IGunUser
{
    public event EventHandler OnReloadGun;
    public event GunFireActionEventDelegate OnFireGun;
    public delegate Gun.fireActionResult GunFireActionEventDelegate(object sender, EventArgs e);
}
