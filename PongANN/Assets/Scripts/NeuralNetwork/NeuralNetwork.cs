
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

    private int m_hiddenLayerNb;
    public int HiddenLayerNb
    {
        get { return m_hiddenLayerNb; }
        protected set { m_hiddenLayerNb = value; }
    }

    private int m_neuronsPerHiddenLayer;
    public int NeuronsPerHiddenLayer
    {
        get { return m_neuronsPerHiddenLayer; }
        protected set { m_neuronsPerHiddenLayer = value; }
    }
    #endregion

    #region Layers
    private NeuronLayer m_inputLayer;
    public NeuronLayer InputLayer
    {
        get { return m_inputLayer; }
        protected set { m_inputLayer = value; }
    }

    private NeuronLayer m_outputLayer;
    public NeuronLayer OutputLayer
    {
        get { return m_outputLayer; }
        protected set { m_outputLayer = value; }
    }

    private NeuronLayer[] m_hiddenLayers;
    public NeuronLayer[] HiddenLayers
    {
        get { return m_hiddenLayers; }
        protected set { m_hiddenLayers = value; }
    }
    #endregion

    public NeuralNetwork( int inputNb, int outputNb, int hiddenLayerNb = 1, int neuronsPerHiddenLayer = 2 )
    {
        if ( inputNb < 1 || outputNb < 1 || hiddenLayerNb < 1 || neuronsPerHiddenLayer < 1 /*2*/ )
            throw new ArgumentException();

        Gain = 1.0f;

        InputNb = inputNb;
        InputLayer = new NeuronLayer( this, InputNb );

        OutputNb = outputNb;
        OutputLayer = new NeuronLayer( this, OutputNb );

        HiddenLayerNb = hiddenLayerNb;
        NeuronsPerHiddenLayer = neuronsPerHiddenLayer;
        HiddenLayers = new NeuronLayer[HiddenLayerNb];
        for ( int i = 0; i < hiddenLayerNb; ++i )
            HiddenLayers[i] = new NeuronLayer( this, NeuronsPerHiddenLayer );

        CreateLinks();
    }

    private void CreateLinks()
    {
        InputLayer.NextLayer = HiddenLayers[0];

        for ( int i = 1; i < HiddenLayerNb; ++i )
            HiddenLayers[i - 1].NextLayer = HiddenLayers[i];

        HiddenLayers[HiddenLayerNb - 1].NextLayer = OutputLayer;
    }

    #region Execution
    private void SetInputs( params float[] inputs )
    {
        if ( inputs.Length != InputNb )
            throw new ArgumentException();

        // Set values for inputs
        for ( int i = 0; i < InputNb; i++ )
            InputLayer.Neurons[i].SetAsInput( inputs[i] );
    }

    private void ExecuteInputLayer()
    {
        HiddenLayers[0].ActivateLayer();
    }

    private void ExecuteHiddenLayers()
    {
        for ( int i = 1; i < HiddenLayerNb; ++i )
        {
            HiddenLayers[i].ActivateLayer();
        }
    }

    private void ExecuteOutputLayer()
    {
        OutputLayer.ActivateLayer();
    }

    public void Execute( params float[] inputs )
    {
        SetInputs( inputs );

        ExecuteInputLayer();
        ExecuteHiddenLayers();
        ExecuteOutputLayer();
    }
    #endregion

    #region Training
    public void Train( float[] inputs, float[] outputsWished )
    {
        if ( outputsWished.Length != OutputNb )
            throw new ArgumentException();

        Execute( inputs );

        if ( true )
        {
            Console.Write( "\n\n-----------------------------------\n" );
            Console.Write( "Inputs" );
            foreach ( float oneInput in inputs )
                Console.Write( "\tInput[{0}]\n", oneInput );
            Console.Write( "\n" );

            Console.Write( "Outputs" );
            for ( int i = 0; i < OutputNb; ++i )
                Console.Write( "\tOutput[{0}] || OutputWished[{1}]\n", OutputLayer.Neurons[i].ActivationValue,
                               outputsWished[i] );
            Console.Write( "\n-----------------------------------\n\n" );
        }

        ComputeBackpropagation( outputsWished );
    }

    private void ComputeFirstOutputBackprop( float[] outputsWished )
    {
        // Manage error with Output values
        for ( int i = 0; i < OutputNb; ++i )
        {
            Neuron oneOutput = OutputLayer.Neurons[i];

            // Modify values of last HiddenLayer using Output values
            for ( int j = 0; j < NeuronsPerHiddenLayer; ++j )
            {
                float delta = -( outputsWished[i] - oneOutput.ActivationValue )
                              * oneOutput.ActivationValue
                              * ( 1.0f - oneOutput.ActivationValue );
                Neuron.Link link = HiddenLayers[HiddenLayerNb - 1].Neurons[j].GetLinkTowards( oneOutput );
                link.Error = delta;
            }
        }
    }

    private void ComputeHiddenLayersProp( float[] outputsWished )
    {
        // Manage error for HiddenLayers values
        for ( int k = HiddenLayerNb - 1; k >= 0; --k )
        {
            for ( int i = 0; i < NeuronsPerHiddenLayer; ++i )
            {
                Neuron oneHidden = HiddenLayers[k].Neurons[i];
                float totalError = 0.0f;

                // Modify values of last HiddenLayer using Output values
                for ( int j = 0; j < oneHidden.NextNeuronsNb; ++j )
                {
                    Neuron.Link link = oneHidden.NextLinks[j];

                    totalError += link.Error * link.Weight;
                }

                for ( int j = 0; j < oneHidden.PreviousNeuronsNb; ++j )
                {
                    float delta = totalError * oneHidden.ActivationValue * ( 1.0f - oneHidden.ActivationValue );
                    Neuron.Link link = oneHidden.PreviousNeurons[j].GetLinkTowards( oneHidden );
                    link.Error = delta;
                }
            }
        }
    }

    public void ComputeErrors()
    {
        foreach ( Neuron oneNeuron in InputLayer.Neurons )
        {
            foreach ( Neuron.Link oneLink in oneNeuron.NextLinks )
            {
                oneLink.Weight -= Gain * oneLink.Error * oneNeuron.ActivationValue;
                oneLink.Error = 0.0f;
            }
        }

        foreach ( NeuronLayer oneLayer in HiddenLayers )
        {
            foreach ( Neuron oneNeuron in oneLayer.Neurons )