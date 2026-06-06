using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    store players weapons
    manage inputs
*/
public class PlayerWeapons : MonoBehaviour
{
    public Weapon[] weapons = new Weapon[3];
    InputAction[] weaponActions = new InputAction[3];
    InputAction mouseAction;
    Action<InputAction.CallbackContext>[] weaponCastHandlers = new Action<InputAction.CallbackContext>[3];

    void Awake()
    {
        // store input actions in array, later link weapon's Cast() to them
        weaponActions[0] = InputSystem.actions.FindAction("UseWeapon1");
        weaponActions[1] = InputSystem.actions.FindAction("UseWeapon2");
        weaponActions[2] = InputSystem.actions.FindAction("UseWeapon3");

        mouseAction = InputSystem.actions.FindAction("Mouse");
    }

    void OnEnable()
    {
        for(int i=0; i<3; i++)
        {
            // need local index var because lambda stores ref to var i, not its value
            int index = i;
            // store delegate object in handler to use when removing delegate.
            weaponCastHandlers[i] = ctx => CastWeapon(index); 
            weaponActions[i].performed += weaponCastHandlers[i];
        }
    }

    void OnDisable()
    {
        for(int i=0; i<3; i++)
        {
            weaponActions[i].performed -= weaponCastHandlers[i];
            
            // weaponActions[i].performed -= ctx => weapons[i].Cast();
            // cannot use above, this creates a new delegate and is not equivalent to the one made earlier.
        }
    }

    void CastWeapon(int index)
    {
        // Mouse.current.position.ReadValue()
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouseAction.ReadValue<Vector2>());
        Vector2 playerPos = transform.position;
        Vector2 fireDir = (mousePos - playerPos).normalized;
        weapons[index].Cast(playerPos, fireDir);
        // TODO: also pass target gameobject into cast
        //       for point and click spells
    }
}