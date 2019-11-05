﻿using UnityEngine;

namespace AimIK
{
    using Properties;
    using Functions;

    public class AimIKBehaviour : AimIKBehaviourBase
    {
        public Part head;
        public Part[] chestParts;

        /// <summary>
        /// Check the clamp
        /// </summary>
        /// <param name="part">The input part transform</param>
        /// <param name="limitRotation">The input limit rotation</param>
        /// <param name="rotation">The input rotation</param>
        private void CheckClamp(Transform part, LimitRotation limitRotation, Rotation rotation)
        {
            // Clamp (If activate)
            if (limitRotation.x.active)
                rotation.x = AimIKFunctions.ClampAngle(part.localEulerAngles.x, limitRotation.x.min, limitRotation.x.max);
            else
                rotation.x = part.localEulerAngles.x;

            if (limitRotation.y.active)
                rotation.y = AimIKFunctions.ClampAngle(part.localEulerAngles.y, limitRotation.y.min, limitRotation.y.max);
            else
                rotation.y = part.localEulerAngles.y;

            if (limitRotation.z.active)
                rotation.z = AimIKFunctions.ClampAngle(part.localEulerAngles.z, limitRotation.z.min, limitRotation.z.max);
            else
                rotation.z = part.localEulerAngles.z;

            // Set rotation variables to part rotation
            Vector3 partRotation = new Vector3(rotation.x, rotation.y, rotation.z);
            part.localEulerAngles = partRotation;
        }
        
        /// <summary>
        /// LateUpdate called after Update and FixedUpdate functions each frames. This function is on top of any animation.
        /// </summary>
        private void LateUpdate()
        {
            // Check the chest parts
            if(chestParts.Length > 0)
            {
                foreach(Part chestPart in chestParts)
                {
                    if(chestPart.part && target) // If chest part and target exists
                    {
                        if (smoothLookAt) // If you checked is smooth option
                            chestPart.part.LookAt3D(smoothTarget - chestPart.positionOffset, chestPart.rotationOffset);
                        else
                            chestPart.part.LookAt3D(target.position - chestPart.positionOffset, chestPart.rotationOffset);
                        
                        CheckClamp(chestPart.part, chestPart.limitRotation, chestPart.GetRotation());
                    }
                }
            }

            // If head and target exists
            if(head.part && target)
            {
                if (smoothLookAt) // If you checked is smooth option
                    head.part.LookAt3D(smoothTarget - head.positionOffset, head.rotationOffset);
                else
                    head.part.LookAt3D(target.position - head.positionOffset, head.rotationOffset);

                CheckClamp(head.part, head.limitRotation, head.GetRotation());
            }
        }
    }
}
