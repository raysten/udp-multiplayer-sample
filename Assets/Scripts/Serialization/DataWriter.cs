using System.IO;
using System.Text;
using UnityEngine;

public class DataWriter
{
    private readonly BinaryWriter _writer;
    private readonly MemoryStream _stream;

    public DataWriter()
    {
        _stream = new MemoryStream();
        _writer = new BinaryWriter(_stream);
    }

    public void Write(int value)
    {
        _writer.Write(value);
    }

	public void Write(uint value)
	{
		_writer.Write(value);
	}

	public void Write(bool value)
	{
		_writer.Write(value);
	}

    public void Write(string value, bool writeLength = true)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        if (writeLength)
        {
            _writer.Write(value.Length);
        }

        _writer.Write(Encoding.ASCII.GetBytes(value));
    }

	public void Write(Vector3 vector)
	{
		_writer.Write(vector.x);
		_writer.Write(vector.y);
		_writer.Write(vector.z);
	}

    public byte[] Finalize()
    {
        _writer.Flush();
        _stream.Flush();

        var tmp = _stream.ToArray();
        _writer.Close();
        _writer.Dispose();
        _stream.Close();
        _stream.Dispose();

        return tmp;
    }
}