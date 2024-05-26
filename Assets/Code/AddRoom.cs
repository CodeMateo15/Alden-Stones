using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("MainRooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }

    public void Fix1()
    {
        Instantiate(templates.ClosedbottomRooms, transform.position, templates.ClosedbottomRooms.transform.rotation);
        Destroy(this.gameObject);
    }

    public void Fix2()
    {
        Instantiate(templates.ClosedtopRooms, transform.position, templates.ClosedtopRooms.transform.rotation);
        Destroy(this.gameObject);
    }

    public void Fix3()
    {
        Instantiate(templates.ClosedleftRooms, transform.position, templates.ClosedleftRooms.transform.rotation);
        Destroy(this.gameObject);
    }

    public void Fix4()
    {
        Instantiate(templates.ClosedrightRooms, transform.position, templates.ClosedrightRooms.transform.rotation);
        Destroy(this.gameObject);
    }

}
