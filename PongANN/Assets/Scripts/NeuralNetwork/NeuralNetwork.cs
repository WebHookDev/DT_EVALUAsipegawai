
﻿using System;

public class NeuralNetwork
{
    #region Architecture
    private float m_gain;
    public float Gain
    {
        get { return m_gain; }
        protected set { m_gain = value; }
    }

    private int m_inputNb;
    public int InputNb
    {
        get { return m_inputNb; }
        protected set { m_inputNb = value; }
    }

    private int m_outputNb;
    public int OutputNb
    {
        get { return m_outputNb; }
        protected set { m_outputNb = value; }
    }