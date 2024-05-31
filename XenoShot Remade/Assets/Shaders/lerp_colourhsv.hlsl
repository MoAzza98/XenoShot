//UNITY_SHADER_NO_UPGRADE
# ifndef MYHLSLINCLUDE_INCLUDED
# define MYHLSLINCLUDE_INCLUDED

// Alan Zucconi :D
// https://www.alanzucconi.com/2016/01/06/colour-interpolation/

void MyFunction_float(float4 a, float4 b, float t, out float4 Out) {
	
	// Hue interpolation
	float hue;
	float d = b.x - a.x;
	if (a.x > b.x)
	{
		// Swap (a.x, b.x)
		float h3 = b.x;
		b.x = a.x;
		a.x = h3;

		d = -d;
		t = 1 - t;
	}

	if (d > 0.5) // 180deg
	{
		a.x = a.x + 1; // 360deg
		hue = (a.x + t * (b.x - a.x)) % 1; // 360deg
	}

	if (d <= 0.5) // 180deg
	{
		hue = a.x + t * d;
	}
	
	// Interpolates the rest
	Out.x = hue; // H
	Out.y = a.y + t * (b.y - a.y); // S
	Out.z = a.z + t * (b.z - a.z); // V
	Out.w = a.w + t * (b.w - a.w); // A
}

#endif