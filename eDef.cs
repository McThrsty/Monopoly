using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum ePiece { car, tophat, dog, cat, battleship, thimble, shoe, wheelbarrow, terminator }
public enum ePlayerType {none, human, AI}
public enum eDifficulty { intern, manager, CEO }
public enum eTypeSpot { doNothing, property, commChest, chance, tax, railroad, utility, goToJail }
public enum ePos { go, mediterranean, commChest1, baltic, incomeTax, readingRR, oriental, chance1, vermont, connecticut,
    justVisiting, stCharles, electricCompany, states, virginia, pennRR, stJames, commChest2, tennessee, newYork, freeParking, 
    kentucky, chance2, indiana, illinois, boRR, atlantic, ventnor, waterWorks, marvinGardens, goToJail, pacific, northCarolina, 
    commChest3, pennsylvania, shortLineRR, chance3, parkPlace, luxuryTax, boardwalk, terminator }

public enum ePropertyGroupColor { none, Brown, Light_Blue, Pink, Orange, Red, Yellow, Green, Dark_Blue }
public enum eCards { chance, communityChest }
public enum eHouseRules { freeParking, goExtraCash }

public enum eCardType {collectReward, payBank, payEachPlayer, goToSpot, giveCard, payHouseHotel }

public enum eChanceCards { card1, card2, card3, card4, card5, card6, card7, card8, card9, card10, card11, card12, card13, card14, card15, card16}

public enum eCommChestCards { card1, card2, card3, card4, card5, card6, card7, card8, card9, card10, card11, card12, card13, card14, card15, card16, card17, card18 }

public enum eDiceRolls { one, two, three, four, five, six}

public enum eAudio { click, noGo, landOnSpot}


public class NamedArrayAttribute : PropertyAttribute
{
	public Type TargetEnum;
	public NamedArrayAttribute(Type TargetEnum)
	{
		this.TargetEnum = TargetEnum;
	}
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer {
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		// Properly configure height for expanded contents.
		return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
	}
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		// Replace label with enum name if possible.
		try {
			var config = attribute as NamedArrayAttribute;
			var enum_names = System.Enum.GetNames(config.TargetEnum);
			int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
			var enum_label = enum_names.GetValue(pos) as string;
			// Make names nicer to read (but won't exactly match enum definition).
			enum_label = ObjectNames.NicifyVariableName(enum_label.ToLower());
			label = new GUIContent(enum_label);
		} catch {
			// keep default label
		}
		EditorGUI.PropertyField(position, property, label, property.isExpanded);
	}
}
#endif






public class eDef : MonoBehaviour
{
    
}
