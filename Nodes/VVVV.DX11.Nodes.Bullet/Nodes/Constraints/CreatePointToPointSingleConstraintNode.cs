﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulletSharp;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VMath;
using VVVV.Bullet.Core;

namespace VVVV.Nodes.Bullet
{
	[PluginInfo(Name="Point2Point",Author="vux",Category="Bullet",Version="Constraint.Single",AutoEvaluate=true)]
	public class CreateSingelPoint2PointConstraintNode : IPluginEvaluate
	{
        [Input("World", Order = 10)]
        protected ISpread<IConstraintContainer> contraintContainer;

        [Input("Body", Order = 11)]
        protected ISpread<RigidBody> FBody;

        [Input("Pivot 1", Order = 12)]
        protected ISpread<Vector3D> FPivot1;

		[Input("Damping", Order = 13)]
        protected ISpread<float> FDamping;

		[Input("Impulse Clamp", Order = 14)]
        protected ISpread<float> FImpulseClamp;

		[Input("Tau", Order = 15)]
        protected ISpread<float> FTau;

        [Input("Do Create", IsBang =true, Order = 15000)]
        protected ISpread<bool> FCreate;

        public void Evaluate(int SpreadMax)
        {
            if (this.contraintContainer[0] != null)
            {
                for (int i = 0; i < SpreadMax; i++)
                {
                    if (FCreate[i])
                    {
                        RigidBody body = FBody[i];
                        if (body != null)
                        {
                            Point2PointConstraint cst = new Point2PointConstraint(body, this.FPivot1[i].ToBulletVector());
                            cst.Setting.Damping = this.FDamping[i];
                            cst.Setting.ImpulseClamp = this.FImpulseClamp[i];
                            cst.Setting.Tau = this.FTau[i];

                            this.contraintContainer[0].AttachConstraint(cst);
                        }
                    }
                }
            }
        }
	}
}
