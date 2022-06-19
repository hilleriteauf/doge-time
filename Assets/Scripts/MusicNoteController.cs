using Assets.Scripts.MIDI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNoteController : MonoBehaviour
{
    float StartTime = -1f;
    float AnimationDuration = 1f;
    Vector3 RelativeDestination;
    Vector3 StartPoint;
    SpriteRenderer SpriteRenderer;
    bool WellPlaced;

    public void StartAnimation(MusicNote MusicNote, bool WellPlaced)
    {
        this.WellPlaced = WellPlaced;
        StartTime = Time.time;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.color = MusicNoteHelper.GetMusicNoteColor(MusicNote);

        if (WellPlaced)
        {
            RelativeDestination = new Vector3((float)Random.Range(15, 20) / 10f, (float)Random.Range(45, 50) / 10f, 0);
        }
        else
        {
            RelativeDestination = new Vector3((float)Random.Range(45, 50) / 7f, (float)Random.Range(15, 20) / 7f, 0);
        }

        StartPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartTime != -1)
        {
            float progression = Time.time - StartTime;

            if (WellPlaced)
            {
                Vector3 NewPosition = Vector3.Lerp(StartPoint, StartPoint + Vector3.right * RelativeDestination.x, progression);
                NewPosition = Vector3.Lerp(NewPosition, StartPoint + RelativeDestination, progression);
                transform.position = NewPosition;
            }
            else
            {
                Vector3 NewPosition = Vector3.Lerp(StartPoint, StartPoint + Vector3.up * RelativeDestination.y, progression);
                NewPosition = Vector3.Lerp(NewPosition, StartPoint + RelativeDestination, progression);
                transform.position = NewPosition;
                transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 1800, progression));
            }

            SpriteRenderer.color = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, Mathf.Lerp(1f, 0, 4f * progression - (3f / 2f)));
        }
    }
}
