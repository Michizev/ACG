using OpenTK;
using System;

namespace Example
{
	class OrbitingCamera
	{
		public OrbitingCamera(float distance, float azimuth = 0f, float elevation = 0f)
		{
			Distance = distance;
			Azimuth = azimuth;
			Elevation = elevation;
		}

		public float Distance
		{
			get => _distance; 
			set
			{
				_distance = MathF.Max(0.001f, value);
				UpdateMatrix();
			}
		}
		public float Azimuth
		{
			get => _azimuth; 
			set
			{
				_azimuth = value;
				UpdateMatrix();
			}
		}
		public float Elevation
		{
			get => _elevation; 
			set
			{
				_elevation = value;
				UpdateMatrix();
			}
		}
		public Matrix4 View { get; private set; } = Matrix4.Identity;

		private float _azimuth;
		private float _distance;
		private float _elevation;

		void UpdateMatrix()
		{
			//TODO: use CameraDistance, CameraAzimuth, CameraElevation to create an orbiting camera
			View = Matrix4.Identity;
#if SOLUTION
			var mtxDistance = Matrix4.CreateTranslation(0, 0, -Distance);
			var mtxElevation = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Elevation));
			var mtxAzimut = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Azimuth));
			View = mtxAzimut * mtxElevation * mtxDistance;
#endif
		}

	}
}
