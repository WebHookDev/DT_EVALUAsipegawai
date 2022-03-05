
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