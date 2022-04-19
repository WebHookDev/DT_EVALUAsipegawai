
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
    public int NextNeuronsNb
    {
        get { return m_nextNeuronsNb; }
        protected set { m_nextNeuronsNb = value; }
    }

    private Link[] m_nextLinks;
    public Link[] NextLinks
    {
        get { return m_nextLinks; }
        protected set { m_nextLinks = value; }
    }
    #endregion
    #endregion

    public Neuron(NeuralNetwork neuralNetwork, bool randomValues = true)
    {
        DebugId = ++_staticId;

        m_neuralNetwork = neuralNetwork;

        if (!randomValues)
            return;

        ActivationValue = Random.Range( 0.0f, 1.0f );
        Bias = 1.0f; // Random.Range( -1.0f, 1.0f );
    }

    public void Activate()
    {
        float activationValue = 0;
        for (int i = 0; i < PreviousNeuronsNb; ++i)
            activationValue += PreviousNeurons[i].ActivationValue
                               * PreviousNeurons[i].GetLinkTowards(this).Weight;
        activationValue *= Bias;
        ActivationValue = Sigmoid(activationValue);
    }

    private float Sigmoid(float val)
    {
        return (float)(1.0f / (1.0f + Math.Exp(-val)));
    }

    private float ReLU(float val)
    {
        return (float)Math.Max(val, 0.01 * val);
    }

    public Link GetLinkTowards(Neuron nextNeuron)
    {
        for (int i = 0; i < NextNeuronsNb; ++i)
        {
            if (NextLinks[i].NextNeuron == nextNeuron)
                return NextLinks[i];
        }

        throw new ArgumentOutOfRangeException();
    }

    #region Layers
    public void SetNextLayer(NeuronLayer nextLayer)
    {
        NextNeuronsNb = nextLayer.NeuronNb;
        NextLinks = new Link[NextNeuronsNb];

        for (int i = 0; i < NextNeuronsNb; ++i)
        {
            NextLinks[i] = new Link
            {
                NextNeuron = nextLayer.Neurons[i],
                Weight = Random.Range(-1.0f, 1.0f),
                Error = 0.0f
            };
        }
    }

    public void SetPreviousLayer(NeuronLayer previousLayer)
    {
        PreviousNeuronsNb = previousLayer.NeuronNb;
        PreviousNeurons = new Neuron[PreviousNeuronsNb];
