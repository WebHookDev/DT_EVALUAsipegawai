
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