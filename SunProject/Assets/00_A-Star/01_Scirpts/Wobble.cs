using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wobble : MonoBehaviour
{
    private TMP_Text tmpTxt;

    private void Awake()
    {
        tmpTxt = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        tmpTxt.ForceMeshUpdate(); // 강제적으로 현재 텍스트에 맞게 메시정보 업데이트

        TMP_TextInfo textInfo = tmpTxt.textInfo; // 문자 정보들
        

        for(int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (charInfo.isVisible == false) continue;

            Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            Color32[] colors = textInfo.meshInfo[charInfo.materialReferenceIndex].colors32;

            int vIdx0 = charInfo.vertexIndex;

            for(int j = 0; j < 4; j++)
            {
                Vector3 origin = vertices[vIdx0 + j];
                vertices[vIdx0 + j] = origin + new Vector3(0, Mathf.Sin(Time.time * 2f + origin.x), 0);
            }

            tmpTxt.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices | TMP_VertexDataUpdateFlags.Colors32);
        }
    
        /*for(int i =0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            tmpTxt.UpdateGeometry(meshInfo.mesh, i);
        }*/
    }
}

/*TMP_CharacterInfo second = textInfo.characterInfo[1]; // 두번ㅉㅐ 글자

Vector3[] vertices = textInfo.meshInfo[second.materialReferenceIndex].vertices;
int vIndex0 = second.vertexIndex;
Debug.Log(second.vertexIndex);

for (int i = 0; i < 4; i++)
{
    Vector3 origin = vertices[vIndex0 + i];
    if (i == 1 || i == 2)
        vertices[vIndex0 + i] = origin + new Vector3(0, 0.5f, 0);
}

var meshfInfo = textInfo.meshInfo[second.materialReferenceIndex];
meshfInfo.mesh.vertices = meshfInfo.vertices;

tmpTxt.UpdateGeometry(meshfInfo.mesh, second.materialReferenceIndex);
*/