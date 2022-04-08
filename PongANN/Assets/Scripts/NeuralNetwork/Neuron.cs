
﻿using System;
using Random = UnityEngine.Random;

public class Neuron
{
    private static int _staticId = -1;
    public readonly int DebugId;
    private NeuralNetwork m_neuralNetwork;

    public class Link
    {
        public Neuron NextNeuron = null;

        private float m_weight = 0.0f;
        public float Weight
        {
            get { return m_weight; }
            set
            {
                //if (value < -1.0f || value > 1.0f)
                //    throw new InvalidOperationException();
                m_weight = value;
            }
        }

        private float m_error = 0.0f;
        public float Error
        {
            get { return m_error; }
            set
            {
                if (value < -2.0f || value > 2.0f)
                    throw new InvalidOperationException();
                m_error = value;
            }
        }
    }

    #region Values
    private float m_activationValue = 0.0f;
    public float ActivationValue
    {
        get { return m_activationValue; }
        set
        {
            if (value < 0.0f || value > 1.0f)
                throw new InvalidOperationException();
            m_activationValue = value;
        }
    }

    private float m_bias = 0.0f;
    public float Bias
    {
        get { return m_bias; }
        set
        {
            if (value < -1.0f || value > 1.0f)
                throw new InvalidOperationException();
            m_bias = value;
        }
    }
    #endregion

    #region Neurons wired
    #region Previous neurons
    private int m_previousNeuronsNb;
    public int PreviousNeuronsNb
    {
        get { return m_previousNeuronsNb; }
        protected set { m_previousNeuronsNb = value; }
    }

    private Neuron[] m_previousNeurons;
    public Neuron[] PreviousNeurons
    {
        get { return m_previousNeurons; }
        protected set { m_previousNeurons = value; }
    }
    #endregion

    #region Next neurons
    private int m_nextNeuronsNb;