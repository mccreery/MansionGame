using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArtefactBook : MonoBehaviour
{
    public Texture[] pageTextures;

    public string forwardFlipState;
    public string backwardFlipState;

    private Animator animator;
    public new Renderer renderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        UpdateTexture();
    }

    public int Page { get; private set; }

    public void NextPage()
    {
        if (Page + 1 < pageTextures.Length)
        {
            ++Page;
            animator.Play(forwardFlipState, -1, 0);
        }
    }

    public void PreviousPage()
    {
        if (Page > 0)
        {
            --Page;
            animator.Play(backwardFlipState, -1, 0);
        }
    }

    public void UpdateTexture()
    {
        renderer.material.mainTexture = pageTextures[Page];
    }
}
