using System;

public interface IGunUser
{
    public event EventHandler OnFireGun;
    public event EventHandler OnReloadGun;
}
