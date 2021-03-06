using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLaserState : IState
{
    public float laserStartSize = 0.1f;
    [SerializeField] float maxWidth1 = 0.1f;
    [SerializeField] float maxWidth2 = 2f;
    List<StationaryLaser> lasers = new List<StationaryLaser>();

    public HorizontalLaserState(LaserRoom room) //constructor
    {
        laserRoom = room;
        canTransition = false;
    }

    public override IEnumerator Enter()
    {
        //17 lasers total
        canTransition = false;

        lasers = laserRoom.GetLaserTurrets(new int[] { 7, 8 });
        Initlaser(lasers);
        ActivateTheLasers(maxWidth1, lasers);
        yield return new WaitForSeconds(2f);
        ActivateTheLasers(maxWidth2, lasers);
        yield return new WaitForSeconds(2f);
        DeActivateTheLasers(lasers);
        Finishedlaser(lasers);

        lasers = laserRoom.GetLaserTurrets(new int[] { 0, 16 });
        Initlaser(lasers);
        ActivateTheLasers(maxWidth1, lasers);
        yield return new WaitForSeconds(2f);
        ActivateTheLasers(maxWidth2, lasers);
        yield return new WaitForSeconds(2f);
        DeActivateTheLasers(lasers);
        Finishedlaser(lasers);

        lasers = laserRoom.GetLaserTurrets(new int[] { 6, 12 });
        Initlaser(lasers);
        ActivateTheLasers(maxWidth1, lasers);
        yield return new WaitForSeconds(2f);
        ActivateTheLasers(maxWidth2, lasers);
        yield return new WaitForSeconds(2f);
        DeActivateTheLasers(lasers);
        Finishedlaser(lasers);


        yield break;
    }

    public void Finishedlaser(List<StationaryLaser> stationaryLasers)
    {
        foreach (StationaryLaser stationaryLaser in stationaryLasers)
        {
            stationaryLaser.Line.enabled = false;
        }

    }

    public void Initlaser(List<StationaryLaser> stationaryLasers)
    {
        foreach (StationaryLaser stationaryLaser in stationaryLasers)
        {
            stationaryLaser.Line.enabled = true;
        }
    }

    public void ActivateTheLasers(float maxsize, List<StationaryLaser> stationaryLasers)
    {
        foreach (StationaryLaser stationaryLaser in stationaryLasers)
        {
            stationaryLaser.IncreaseLaserSize(maxsize);
        }
    }
    public void DeActivateTheLasers(List<StationaryLaser> stationaryLasers)
    {
        foreach (StationaryLaser stationaryLaser in stationaryLasers)
        {
            stationaryLaser.DecreaseLaserSize();
        }
    }
}
