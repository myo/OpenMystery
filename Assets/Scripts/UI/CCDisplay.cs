using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCDisplay : MonoBehaviour
{

    [SerializeField] CustomizeAvatarGroup customizeAvatarGroup;

    [SerializeField] GameObject robesSubButtons;
    [SerializeField] GameObject headSubButtons;
    [SerializeField] GameObject miscSubButtons;

    [SerializeField] GameObject nextPreviewButton;
    [SerializeField] GameObject previousPreviewButton;

    [SerializeField] GameObject FaceGroup;
    [SerializeField] GameObject NoseGroup;
    [SerializeField] GameObject EyesGroup;
    [SerializeField] GameObject BrowsGroup;
    [SerializeField] GameObject LipsGroup;
    [SerializeField] GameObject HairGroup;



    private void Start()
    {
        robesSubButtons.SetActive(true);
        headSubButtons.SetActive(false);
        miscSubButtons.SetActive(false);
    }

    public void setAvatarBuilderGroup(string group)
    {

        switch (group) {

            case "robes":
                robesSubButtons.SetActive(true);
                headSubButtons.SetActive(false);
                miscSubButtons.SetActive(false);
                CustomizeAvatar.setCameraBody();
                break;
            case "head":
                robesSubButtons.SetActive(false);
                headSubButtons.SetActive(true);
                miscSubButtons.SetActive(false);
                CustomizeAvatar.setCameraHead();
                break;
            case "misc":
                robesSubButtons.SetActive(false);
                headSubButtons.SetActive(false);
                miscSubButtons.SetActive(true);
                CustomizeAvatar.setCameraHead();
                break;
        }
    }

    public void hideGroups()
    {
        BrowsGroup.SetActive(false);
        EyesGroup.SetActive(false);
        FaceGroup.SetActive(false);
        NoseGroup.SetActive(false);
        LipsGroup.SetActive(false);
        HairGroup.SetActive(false);
    }
    public void hideRenderPreviews()
    {
        hideGroups();
        customizeAvatarGroup.hidePreviews();
        nextPreviewButton.SetActive(false);
        previousPreviewButton.SetActive(false);
    }

    public void showOnePieceGroup()
    {
        hideGroups();
        customizeAvatarGroup.renderOnePieceComponents();
        nextPreviewButton.SetActive(true);
        previousPreviewButton.SetActive(true);
    }

    public void showTopGroup()
    {
        hideGroups();
        customizeAvatarGroup.renderTopsComponents();
        nextPreviewButton.SetActive(true);
        previousPreviewButton.SetActive(true);
    }

    public void showBottomGroup()
    {
        hideGroups();
        customizeAvatarGroup.renderBottomsComponents();
        nextPreviewButton.SetActive(true);
        previousPreviewButton.SetActive(true);
    }

    public void showHairGroup()
    {
        hideGroups();
        customizeAvatarGroup.renderHairComponents();
        nextPreviewButton.SetActive(true);
        previousPreviewButton.SetActive(true);
        HairGroup.SetActive(true);
    }

    public void showBrowsGroup()
    {
        hideRenderPreviews();
        hideGroups();
        BrowsGroup.SetActive(true);
    }

    public void showEyesGroup()
    {
        hideGroups();
        customizeAvatarGroup.renderEyeComponents();
        nextPreviewButton.SetActive(false);
        previousPreviewButton.SetActive(false);
        EyesGroup.SetActive(true);
    }

    public void showNoseGroup()
    {
        hideRenderPreviews();
        hideGroups();
        NoseGroup.SetActive(true);
    }

    public void showLipsGroup()
    {
        hideGroups();
        customizeAvatarGroup.renderLipsComponents();
        nextPreviewButton.SetActive(false);
        previousPreviewButton.SetActive(false);
        LipsGroup.SetActive(true);
    }

    public void showFaceGroup()
    {
        hideRenderPreviews();
        hideGroups();
        FaceGroup.SetActive(true);
    }

    public void showFacePaintGroup()
    {
        hideGroups();
        customizeAvatarGroup.renderFacePaintComponents();
        nextPreviewButton.SetActive(true);
        previousPreviewButton.SetActive(true);
    }

}
